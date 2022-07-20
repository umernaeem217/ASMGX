namespace ASMGX.DeepMed.Shared.Hashing.Interfaces
{
    public interface IPasswordHasher
    {
        string Hash(string password);

        (bool Verified, bool NeedsUpgrade) Check(string hash, string password);
    }
}
