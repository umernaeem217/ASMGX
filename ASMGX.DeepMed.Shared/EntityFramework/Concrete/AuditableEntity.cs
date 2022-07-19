using ASMGX.DeepMed.Shared.EntityFramework.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ASMGX.DeepMed.Shared.EntityFramework.Concrete
{
    public class AuditableEntity : IAuditableEntity
    {
        [Key]
        public string Id { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
