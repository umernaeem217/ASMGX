using ASMGX.DeepMed.Shared.EntityFramework.Concrete;

namespace ASMGX.DeepMed.Infrastructure.Models
{
    public class User: BaseEntity
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;

    }
}
