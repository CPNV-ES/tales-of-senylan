using System;

namespace TalesOfSenylan
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using (var game = new TOSGame())
            {
                game.Run();
            }
        }
    }
}