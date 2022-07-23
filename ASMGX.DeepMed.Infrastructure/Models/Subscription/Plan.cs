using ASMGX.DeepMed.Shared.EntityFramework.Concrete;

namespace ASMGX.DeepMed.Infrastructure.Models.Subscription
{
    public class Plan: AuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public int ReportsPerMonth { get; set; } = 0;
        public double Price { get; set; }
        public string Duration { get; set; } = "";
        public bool IsActive { get; set; } = false;
        public string IcdScheme { get; set; } = string.Empty;
    }
}
