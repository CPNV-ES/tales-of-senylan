﻿using System;

namespace TalesOfSenylan.Models.Utilities
{
    public static class Utilities
    {
        public static int GetRandomNumber(int from, int to)
        {
            Random r = new Random();
            return r.Next(from, to);
        }
    }
}
