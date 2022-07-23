using ASMGX.DeepMed.Infrastructure.Contexts;
using ASMGX.DeepMed.Infrastructure.Models.General;
using ASMGX.DeepMed.Shared.Exceptions.Concrete;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ASMGX.DeepMed.Application.General
{
    public class LookupRepository: ILookupRepository
    {
        private readonly ApplicationDbContext _context;

        public LookupRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Lookup?> GetLookupByName(string name)
        {
            return await _context.Lookups.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<T?> ParseLookupToType<T>(string name)
        {
            var lookup = await GetLookupByName(name);
            if (lookup == null)
                throw new UserFriendlyException($"Not lookup found in the database with the name {name}");
            try
            {
                return JsonConvert.DeserializeObject<T>(lookup.Value) ?? throw new NullReferenceException();
            }
            catch (Exception)
            {
                throw new UserFriendlyException($"Error in parsing the {name} lookup.");
            }
        }

        public async Task<IList<Lookup>> GetLookupsByGroup(LookupType type)
        {
            return await _context.Lookups.Where(x => x.Group == type).ToListAsync();
        }

        public async Task<string> GetStringLookup(string name)
        {
            var lookup = await GetLookupByName(name);
            if (lookup == null)
                throw new UserFriendlyException($"Not lookup found in the database with the name {name}");
            return lookup.Value;
        }
    }
}
