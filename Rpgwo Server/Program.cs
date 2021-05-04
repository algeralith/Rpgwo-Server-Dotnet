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


            /*
            Console.WriteLine("6");
            Console.WriteLine();

            PrintElevation(82, 243);
            PrintElevation(83, 244);
            PrintElevation(84, 245);
            PrintElevation(85, 246);
            PrintElevation(86, 247);
            PrintElevation(87, 248);
            PrintElevation(88, 249);
            PrintElevation(90, 251);
            PrintElevation(108, 206);

            Console.WriteLine("7");
            Console.WriteLine();
            PrintElevation(58, 243);
            PrintElevation(51, 250);
            PrintElevation(120, 380);
            PrintElevation(119, 377);
            PrintElevation(101, 219);
            PrintElevation(93, 227);
            PrintElevation(100, 207);

            Console.WriteLine("8");
            Console.WriteLine();
            PrintElevation(83, 209);
            PrintElevation(84, 210);
            PrintElevation(90, 216);
            PrintElevation(95, 212);
            PrintElevation(92, 215);
            PrintElevation(68, 243);

            Console.WriteLine("9");
            Console.WriteLine();

            PrintElevation(96, 213);
            PrintElevation(99, 216);
            PrintElevation(101, 218);
            PrintElevation(103, 219);
            PrintElevation(170, 218);
            PrintElevation(169, 217);
            PrintElevation(167, 215);

            Console.WriteLine("10");
            Console.WriteLine("Does not seem to be used.\r\n");

            Console.WriteLine("11");
            Console.WriteLine();

            PrintElevation(58, 242);
            PrintElevation(51, 249); 
            PrintElevation(51, 237);
            PrintElevation(54, 235);
            PrintElevation(53, 216);
            PrintElevation(50, 219);

            Console.WriteLine("12");
            Console.WriteLine();

            PrintElevation(135, 208);
            PrintElevation(139, 204);
            PrintElevation(137, 206);
            PrintElevation(147, 209);
            PrintElevation(144, 212);
            PrintElevation(122, 382);
            PrintElevation(121, 380);

            Console.WriteLine("13");
            Console.WriteLine();

            PrintElevation(171, 218); 
            PrintElevation(172, 217);
            PrintElevation(177, 206);
            PrintElevation(175, 208);
            PrintElevation(206, 215);
            PrintElevation(215, 216);

            Console.WriteLine("14");
            Console.WriteLine();

            PrintElevation(208, 215);
            PrintElevation(209, 216);
            PrintElevation(208, 215);
            PrintElevation(213, 216);
            PrintElevation(204, 225);

            Console.WriteLine("15");
            Console.WriteLine();

            PrintElevation(314, 492);

            Console.WriteLine("16");
            Console.WriteLine();

            PrintElevation(308, 483);
            PrintElevation(311, 486);
            PrintElevation(310, 485);
            PrintElevation(313, 487);
            PrintElevation(315, 488);
            PrintElevation(316, 492);


            Console.WriteLine("17");
            Console.WriteLine();

            PrintElevation(317, 492); 
            PrintElevation(311, 496);
            PrintElevation(317, 492);
            PrintElevation(319, 496);
            PrintElevation(313, 494);
            PrintElevation(226, 558);
            PrintElevation(224, 561);
            PrintElevation(223, 560);

            Console.WriteLine("18");
            Console.WriteLine();

            PrintElevation(220, 564);
            PrintElevation(222, 562);
            PrintElevation(221, 563);
            PrintElevation(300, 503);
            PrintElevation(297, 507);
            PrintElevation(310, 496);
            PrintElevation(313, 492);
            PrintElevation(314, 493);
            PrintElevation(316, 493);


            Console.WriteLine("19");
            Console.WriteLine();

            PrintElevation(306, 502);
            PrintElevation(305, 504);
            PrintElevation(308, 507);
            PrintElevation(220, 568);
            PrintElevation(224, 562);
            PrintElevation(229, 557);
            PrintElevation(229, 557);
            PrintElevation(231, 558);
            PrintElevation(236, 554);
            PrintElevation(237, 555);


            World.World.GetElevationTile(142, 165, 0);
            */


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