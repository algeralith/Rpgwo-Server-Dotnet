using System;
using Rpgwo_Server.Importer;

namespace Rpgwo_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            // Importer Code. For now.
            MapImporter mapImporter = new MapImporter(@"C:\Users\Mark\Documents\server2\maps");
            mapImporter.Import();

            ItemImporter itemImporter = new ItemImporter(@"C:\Users\Mark\Documents\server2\data");
            // itemImporter.Import();

            Server s = new Server();
        }

        public static void PrintElevation(int x, int y)
        {
            var initial = World.World.GetElevation(x, y, 0);

            for (int y2 = y - 1; y2 <= y + 1; y2++)
            {
                for (int x2 = x - 1; x2 <= x + 1; x2++)
                {
                    Console.Write(World.World.GetElevation(x2, y2, 0) - initial + " " );
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}