using System.Security.Cryptography;

namespace TalesOfSenylan.Models.Utilities
{
    public static class Utilities
    {
        public static int getRandomNumber(int to)
        {
            return RandomNumberGenerator.GetInt32(to);
        }
    }
}
