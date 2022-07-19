using ASMGX.DeepMed.Shared.EntityFramework.Interfaces;

namespace ASMGX.DeepMed.Shared.EntityFramework.Concrete
{
    public class SoftDeletedEntity : ISoftDeletedEntity, IBaseEntity
    {
        public string Id { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
    }
}
