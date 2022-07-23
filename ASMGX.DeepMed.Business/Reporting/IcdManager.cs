using ASMGX.DeepMed.Application.General;
using ASMGX.DeepMed.Infrastructure.Models.Reporting;
using ASMGX.DeepMed.Model.General;
using ASMGX.DeepMed.Shared.Constants;

namespace ASMGX.DeepMed.Business.Reporting
{
    public class IcdManager : IIcdManager
    {
        private readonly ILookupRepository _lookupRepository;

        public IcdManager(ILookupRepository lookupRepository)
        {
            _lookupRepository = lookupRepository;
        }

        public async Task<IList<LookupDto>> GetIcdSchemes()
        {
            var schemes = await _lookupRepository.ParseLookupToType<IcdReport>(Constants.ParitionKeys.ICD_SCHEME);
            return schemes?.Reports.Select(x => new LookupDto()
            {
                Name = x,
                Value = x
            }).ToList() ?? new List<LookupDto>();
        }
    }
}
