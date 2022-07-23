namespace ASMGX.DeepMed.Model.Authentication
{
    public class LoginResponseDto
    {
        public string? Token { get; set; }
        public bool IsVerified { get; set; } = false;
    }
}
