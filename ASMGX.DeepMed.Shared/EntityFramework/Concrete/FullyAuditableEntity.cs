using ASMGX.DeepMed.Shared.EntityFramework.Interfaces;

namespace ASMGX.DeepMed.Shared.EntityFramework.Concrete
{
    public class FullyAuditableEntity : ISoftDeletedEntity, IAuditableEntity
    {
        public string Id { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
