using ASMGX.DeepMed.Infrastructure.Models.General;

namespace ASMGX.DeepMed.Application.General
{
    public interface ILookupRepository
    {
        Task<IList<Lookup>> GetLookupsByGroup(LookupType type);
        Task<Lookup?> GetLookupByName(string name);
        Task<T?> ParseLookupToType<T>(string name);
        Task<string> GetStringLookup(string name);
    }
}
