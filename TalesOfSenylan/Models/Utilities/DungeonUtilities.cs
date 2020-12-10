using Microsoft.Xna.Framework;
using TalesOfSenylan.Models.Dungeon;

namespace TalesOfSenylan.Models.Utilities
{
    public class DungeonUtilities
    {
        public static int GetNumberOfEnemies(int dungeonNumber)
        {
            return dungeonNumber * 5;
        }

        public static CardinalPoint? GetExitFromPosition(Vector2 position)
        {
            if (position.X <= 0)
            {
                return CardinalPoint.WEST;
            }

            if (position.X >= Constants.GameWidth)
            {
                return CardinalPoint.EAST;
            }

            if (position.Y <= 0)
            {
                return CardinalPoint.NORTH;
            }

            if (position.Y >= Constants.GameHeight)
            {
                return CardinalPoint.SOUTH;
            }

            return null;
        }
    }
}