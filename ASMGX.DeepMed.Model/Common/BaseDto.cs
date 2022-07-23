using ASMGX.DeepMed.Model.Common.Interfaces;

namespace ASMGX.DeepMed.Model.Common
{
    public class BaseDto: IBaseDto
    {
        public string Id { get; set; } = string.Empty;
    }
}
