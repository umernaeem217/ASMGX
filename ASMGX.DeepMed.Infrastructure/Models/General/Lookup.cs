using ASMGX.DeepMed.Shared.EntityFramework.Concrete;
using Microsoft.EntityFrameworkCore;

namespace ASMGX.DeepMed.Infrastructure.Models.General
{
    [Index(nameof(Name), nameof(Group), IsUnique =true)]
    public class Lookup: BaseEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public LookupType Group { get; set; }

    }
}
