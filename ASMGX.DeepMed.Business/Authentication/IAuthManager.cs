using ASMGX.DeepMed.Model.Authentication;

namespace ASMGX.DeepMed.Business.Authentication
{
    public interface IAuthManager
    {
        public Task<string> Signup(SignupDto signupDto);
        public Task Login(LoginDto loginDto);
    }
}
