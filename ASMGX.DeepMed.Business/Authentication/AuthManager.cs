using ASMGX.DeepMed.Application.Shared.Domain.Interfaces;
using ASMGX.DeepMed.Infrastructure.Models;
using ASMGX.DeepMed.Model.Authentication;
using ASMGX.DeepMed.Shared.Exceptions.Concrete;
using ASMGX.DeepMed.Shared.Hashing.Interfaces;
using ASMGX.DeepMed.Shared.Utilities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ASMGX.DeepMed.Business.Authentication
{
    public class AuthManager : IAuthManager
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;

        public AuthManager(IRepository<User> userRepository, IMapper mapper, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        private async Task<bool> Authenticate(string email, string password)
        {
            var user = await _userRepository.Find(x => x.Email == email.Trim().ToLower()).FirstOrDefaultAsync();
            if (user != null)
            {
                var (Verified, NeedsUpgrade) = _passwordHasher.Check(user.Password, password);
                return Verified;
            }
            return false;
        }

        public async Task Login(LoginDto loginDto)
        {
            var isEmail = ValidationUtilities.IsValidEmail(loginDto.Identity);
            bool isAuthenticated;
            if (isEmail)
                isAuthenticated = await Authenticate(loginDto.Identity, loginDto.Password);
            else
            {
                //TODO: Need to write the case to handle usernames.
                throw new NotImplementedException();
            }
            if (!isAuthenticated)
                throw new UserFriendlyException("Email or password is incorrect.");
        }

        public async Task<string> Signup(SignupDto signupDto)
        {
            signupDto.Email = signupDto.Email.Trim().ToLower();
            if (await _userRepository.Find(x => x.Email.Trim().ToLower() == signupDto.Email).AnyAsync())
                throw new UserFriendlyException("User already exists with this email address.");
            var user =  _mapper.Map<User>(signupDto);
            user.Password = _passwordHasher.Hash(user.Password);
            user.Id = Guid.NewGuid().ToString();
            _userRepository.Add(user);
            await _userRepository.SaveChangesAsync();
            return user.Id;
        }
    }
}
