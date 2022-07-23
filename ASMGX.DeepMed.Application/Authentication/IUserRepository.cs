using ASMGX.DeepMed.Application.Shared.Domain.Interfaces;
using ASMGX.DeepMed.Infrastructure.Models.Authentication;

namespace ASMGX.DeepMed.Application.Authentication
{
    public interface IUserRepository: IRepository<User>
    {
        Task<User?> FindUser(string identity);
    }
}
