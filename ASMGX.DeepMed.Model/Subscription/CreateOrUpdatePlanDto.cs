using ASMGX.DeepMed.Model.Common;
using System.ComponentModel.DataAnnotations;

namespace ASMGX.DeepMed.Model.Subscription
{
    public class CreateOrUpdatePlanDto: BaseDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MinLength(1)]
        public int ReportsPerMonth { get; set; } = 0;
        [Required]
        public double Price { get; set; }
        [Required]
        public string Duration { get; set; } = "";
        [Required]
        public bool IsActive { get; set; } = false;
        [Required]
        public string IcdScheme { get; set; } = string.Empty;
    }
}
