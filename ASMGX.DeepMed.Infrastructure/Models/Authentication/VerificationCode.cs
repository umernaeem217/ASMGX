using ASMGX.DeepMed.Shared.EntityFramework.Concrete;

namespace ASMGX.DeepMed.Infrastructure.Models.Authentication
{
    public class VerificationCode: BaseEntity
    {
        public string Code { get; set; }
        public DateTime Expiry { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public VerificationType Type { get; set; }

        public virtual User User { get; set; }
    }
}
