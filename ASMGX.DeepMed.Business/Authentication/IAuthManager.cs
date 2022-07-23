using ASMGX.DeepMed.Model.Authentication;

namespace ASMGX.DeepMed.Business.Authentication
{
    public interface IAuthManager
    {
        Task<string> Signup(SignupDto signupDto);
        Task<LoginResponseDto> Login(LoginDto loginDto);
        Task RequestVerificationCode(string userId, string email = "", string name = "");
        Task<bool> VerifyCode(VerifyCodeDto verifyCodeDto);
    }
}
