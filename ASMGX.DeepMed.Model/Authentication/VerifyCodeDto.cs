namespace ASMGX.DeepMed.Model.Authentication
{
    public class VerifyCodeDto
    {
        public string Code { get; set; } = string.Empty;
        public string OrganizationName { get; set; } = string.Empty;
        public string IcdScheme { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
    }
}
