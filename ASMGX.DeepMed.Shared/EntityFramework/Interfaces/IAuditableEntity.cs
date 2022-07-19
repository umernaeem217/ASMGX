namespace ASMGX.DeepMed.Shared.EntityFramework.Interfaces
{
    public interface IAuditableEntity: IBaseEntity
    {
        public string CreatedBy { get; set; } 
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}
