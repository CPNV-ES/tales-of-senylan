﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TalesOfSenylan.Models.Utilities
{
    public class DungeonUtilities
    {
        public static int GetNumberOfEnemies(int dungeonNumber)
        {
            return dungeonNumber * 5;
        }
    }
}
