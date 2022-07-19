namespace ASMGX.DeepMed.Shared.EntityFramework.Interfaces
{
    public interface ISoftDeletedEntity
    {
        public bool IsDeleted { get; set; }
    }
}
