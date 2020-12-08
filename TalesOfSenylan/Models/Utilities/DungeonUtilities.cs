namespace TalesOfSenylan.Models.Utilities
{
    public class DungeonUtilities
    {
        public static int GetNumberOfEnemies(int dungeonNumber)
        {
            return dungeonNumber * 5;
        }

        public static int GetNumberOfRooms(int dungeonNumber)
        {
            return dungeonNumber * 3;
        }
    }
}