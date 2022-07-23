namespace ASMGX.DeepMed.Shared.Utilities
{
    public static class CommonUtilities
    {
        public static int GetUniqueTimestamp()
        {
            return (int)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds;
        }
        public static int GetRandomNumber(int? start = null, int end= 10)
        {
            Random random = new Random();
            return start.HasValue ? random.Next(start.Value, end) : random.Next(end);
        }
    }
}
