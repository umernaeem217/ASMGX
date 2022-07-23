using ASMGX.DeepMed.Business.Reporting;
using ASMGX.DeepMed.Model.General;
using ASMGX.DeepMed.Shared.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASMGX.DeepMed.WebApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IcdSchemeController : ControllerBase
    {
        private readonly IIcdManager _icdManager;

        public IcdSchemeController(IIcdManager icdManager)
        {
            _icdManager = icdManager;
        }

        [HttpGet]
        public async Task<ActionResult<Response<IList<LookupDto>>>> Get()
        {
            return Ok(new Response<IList<LookupDto>>(await _icdManager.GetIcdSchemes()));
        }
    }
}
