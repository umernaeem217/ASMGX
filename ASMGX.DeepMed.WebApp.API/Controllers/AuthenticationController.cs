using ASMGX.DeepMed.Business.Authentication;
using ASMGX.DeepMed.Model.Authentication;
using ASMGX.DeepMed.Shared.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASMGX.DeepMed.WebApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthManager _authManager;

        public AuthenticationController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        [HttpPost("Signup")]
        public async Task<ActionResult<Response<string>>> Signup(SignupDto signupDto)
        {
            return Ok(new Response<string>(await _authManager.Signup(signupDto), message:"User created successfully."));
        }

        [HttpPost("Login")]
        public async Task<ActionResult<Response<LoginResponseDto>>> Login(LoginDto loginDto)
        {
            return Ok(new Response<LoginResponseDto>(await _authManager.Login(loginDto), message: "User authenticated successfully."));
        }

        [HttpPost("Verify")]
        public async Task<ActionResult<Response<bool>>> Verify(VerifyCodeDto verifyCodeDto)
        {
            return Ok(new Response<bool>(await _authManager.VerifyCode(verifyCodeDto), message: "User verified successfully."));
        }

        [HttpGet("RequestCode/{id}")]
        public async Task<ActionResult<Response<string>>> RequestCode(string id)
        {
            await _authManager.RequestVerificationCode(id);
            return Ok(new Response<string>(message: "Code sent successfully."));
        }
    }
}
