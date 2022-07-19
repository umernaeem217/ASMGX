using ASMGX.DeepMed.Shared.EntityFramework.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ASMGX.DeepMed.Shared.EntityFramework.Concrete
{
    public class BaseEntity: IBaseEntity
    {
        [Key]
        public string Id { get; set; } = string.Empty;
    }
}
