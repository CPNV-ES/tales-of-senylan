using System;

namespace TalesOfSenylan.Models.Utilities
{
    public static class Utilities
    {
        public static int GetRandomNumber(int from, int to)
        {
            var r = new Random(Guid.NewGuid().GetHashCode());
            return r.Next(from, to);
        }
    }
}