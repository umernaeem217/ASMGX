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
        public async Task<ActionResult<Response<bool>>> Login(LoginDto loginDto)
        {
            await _authManager.Login(loginDto);
            return Ok(new Response<bool>(true, message: "User authenticated Successfully."));
        }
    }
}
