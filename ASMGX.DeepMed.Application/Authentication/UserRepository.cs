using ASMGX.DeepMed.Application.Shared.Domain.Concrete;
using ASMGX.DeepMed.Infrastructure.Contexts;
using ASMGX.DeepMed.Infrastructure.Models.Authentication;
using ASMGX.DeepMed.Shared.Utilities;
using Microsoft.EntityFrameworkCore;

namespace ASMGX.DeepMed.Application.Authentication
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> FindUser(string identity)
        {
            var isEmail = ValidationUtilities.IsValidEmail(identity);
            return isEmail ? await _context.Users.Where(x => x.Email == identity.Trim().ToLower()).FirstOrDefaultAsync() :
                await _context.Users.Where(x => x.UserName == identity.Trim().ToLower()).FirstOrDefaultAsync();
        }
    }
}
