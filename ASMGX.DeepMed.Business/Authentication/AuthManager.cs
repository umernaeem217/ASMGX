using ASMGX.DeepMed.Application.Authentication;
using ASMGX.DeepMed.Application.General;
using ASMGX.DeepMed.Application.Shared.Domain.Interfaces;
using ASMGX.DeepMed.Infrastructure.Models.Authentication;
using ASMGX.DeepMed.Model.Authentication;
using ASMGX.DeepMed.Shared.Constants;
using ASMGX.DeepMed.Shared.Exceptions.Concrete;
using ASMGX.DeepMed.Shared.Hashing.Interfaces;
using ASMGX.DeepMed.Shared.Utilities;
using ASMGX.Utilities.SMTP;
using ASMGX.Utilities.SMTP.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ASMGX.DeepMed.Business.Authentication
{
    public class AuthManager : IAuthManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ILookupRepository _lookupRepository;
        private readonly IRepository<VerificationCode> _verificationRepository;
        private readonly IConfiguration _configuration;

        public AuthManager(
            IUserRepository userRepository,
            IMapper mapper,
            IPasswordHasher passwordHasher,
            IRepository<VerificationCode> verificationRepository,
            ILookupRepository lookupRepository,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _verificationRepository = verificationRepository;
            _lookupRepository = lookupRepository;
            _configuration = configuration;
        }

        private async Task SendVerificationEmail(int code, string email, string name)
        {
            var configuration = await _lookupRepository.ParseLookupToType<SmtpClientOptions>(Constants.ParitionKeys.DEFAULT_SMTP_OPTIONS);
            await EmailSender.SendEmail(configuration, new SendEmailParams()
            {
                FromAddress = await _lookupRepository.GetStringLookup(Constants.ParitionKeys.DEFAULT_VERIFICATION_EMAIL_ADDRESS),
                Subject = await _lookupRepository.GetStringLookup(Constants.ParitionKeys.DEFAULT_VERIFICATION_SUBJECT),
                IsHtml = true,
                IsView = false,
                Message = "Your verification code is " + code,
                RecieverName = name,
                SenderName = "ASMGX DeepMed",
                ToAddresses = email
            });
        }

        private async Task<(User? User, bool IsAuthenticated)> Authenticate(string identity, string password)
        {
            var user = await _userRepository.FindUser(identity);
            if (user != null)
            {
                return (user,_passwordHasher.Check(user.Password, password).Verified);
            }
            return (null, false);
        }

        private async Task UpdateVerifiedUser(VerifyCodeDto verifyCodeDto)
        {
            var user = await _userRepository.GetByIdAsync(verifyCodeDto.UserId);
            if(user != null)
            {
                user.IsVerified = true;
                user.OrganizationName = verifyCodeDto.OrganizationName;
                user.IcdScheme = verifyCodeDto.IcdScheme;
                _userRepository.Edit(user);
                await _userRepository.SaveChangesAsync();
            }
        }

        private string GenerateBearerToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
              {
                 new Claim(ClaimTypes.Name, user.FullName),
                 new Claim(ClaimTypes.Email, user.Email),
                 new Claim(ClaimTypes.NameIdentifier, user.Id)
              }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            return tokenHandler.WriteToken(token);
        }

        public async Task<LoginResponseDto> Login(LoginDto loginDto)
        {
            var authenticationResponse = await Authenticate(loginDto.Identity, loginDto.Password);
            if (!authenticationResponse.IsAuthenticated)
                throw new UserFriendlyException("Email or password is incorrect.");
            return new LoginResponseDto()
            {
                IsVerified = authenticationResponse.User.IsVerified,
                Token = GenerateBearerToken(authenticationResponse.User)
            };
        }

        public async Task<string> Signup(SignupDto signupDto)
        {
            signupDto.Email = signupDto.Email.Trim().ToLower();
            if (await _userRepository.Find(x => x.Email.Trim().ToLower() == signupDto.Email).AnyAsync())
                throw new UserFriendlyException("User already exists with this email address.");
            var user =  _mapper.Map<User>(signupDto);
            user.Password = _passwordHasher.Hash(user.Password);
            user.Id = Guid.NewGuid().ToString();
            user.UserName = $"{user.Email.Split("@")[0]}{CommonUtilities.GetRandomNumber()}{CommonUtilities.GetUniqueTimestamp()}";
            _userRepository.Add(user);
            await _userRepository.SaveChangesAsync();
            await RequestVerificationCode(user.Id, user.Email, user.FullName);
            return user.Id;
        }

        public async Task RequestVerificationCode(string userId, string email = "", string name = "")
        {
            if (await _verificationRepository.Find(x => x.Type== VerificationType.EMAIL_CONFIRMATION
                && x.UserId == userId
                && x.CreatedOn.AddMinutes(15) > DateTime.UtcNow)
                .AnyAsync())
                throw new UserFriendlyException("A verification code has been already sent. Please wait for 15 minutes before requesting another code.");
            
            if (string.IsNullOrEmpty(email))
            {
                var user = await _userRepository.GetByIdAsync(userId);
                if (user == null) throw new UserFriendlyException("No user found with this id.");
                email = user.Email;
                name = user.FullName;
            }

            var code = CommonUtilities.GetRandomNumber(100000, 999999);
            var codePayload = new VerificationCode()
            {
                Id = Guid.NewGuid().ToString(),
                Code = code.ToString(),
                Expiry = DateTime.UtcNow.AddDays(1),
                Type = VerificationType.EMAIL_CONFIRMATION,
                UserId = userId,
                CreatedOn = DateTime.UtcNow
            };
            _verificationRepository.Add(codePayload);
            await _verificationRepository.SaveChangesAsync();            
            //await SendVerificationEmail(code, email, name);

        }

        public async Task<bool> VerifyCode(VerifyCodeDto verifyCodeDto)
        {
            if (await _verificationRepository.Find(x => x.UserId == verifyCodeDto.UserId
                && x.Type == VerificationType.EMAIL_CONFIRMATION
                && x.Code == verifyCodeDto.Code
                && x.Expiry.AddDays(1) <= DateTime.UtcNow).AnyAsync())
            {
                var existingCodes = await _verificationRepository.Find(x => 
                x.UserId == verifyCodeDto.UserId
                && x.Type== VerificationType.EMAIL_CONFIRMATION).ToListAsync();
                _verificationRepository.RemoveRange(existingCodes);
                await _verificationRepository.SaveChangesAsync();
                await UpdateVerifiedUser(verifyCodeDto);
                return true;
            }
            return false;
        }
    }
}
