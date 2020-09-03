using System;

namespace TalesOfSenylan
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new TOSGame())
                game.Run();
        }
    }
}
