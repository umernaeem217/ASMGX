using ASMGX.DeepMed.Model.General;

namespace ASMGX.DeepMed.Business.Reporting
{
    public interface IIcdManager
    {
        Task<IList<LookupDto>> GetIcdSchemes();
    }
}
