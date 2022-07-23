using ASMGX.DeepMed.Shared.EntityFramework.Concrete;

namespace ASMGX.DeepMed.Infrastructure.Models.Authentication
{
    public class User : BaseEntity
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public bool IsVerified { get; set; } = false;
        public bool IsOnBoarded { get; set; } = false;
        public string? OrganizationName { get; set; }
        public string? IcdScheme { get; set; }

        public virtual ICollection<VerificationCode> VerificationCodes { get; set; }
    }
}
