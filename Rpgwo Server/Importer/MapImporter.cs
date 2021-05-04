using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;
using Rpgwo_Server.World;

namespace Rpgwo_Server.Importer
{
    public class MapImporter
    {
        private readonly string _mapFolder;

        public MapImporter(string mapFolder)
        {
            _mapFolder = mapFolder;
        }

        public void Import()
        {
            try
            {
                string[] files = Directory.GetFiles(_mapFolder);

                string[] mapFiles = files.Where(path => new Regex(@"^map(\d)*x(\d)*x(\d)*.map$", RegexOptions.IgnoreCase).IsMatch(Path.GetFileName(path))).ToArray();

                foreach(var mapFile in mapFiles)
                {
                    ReadMap(mapFile);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void ReadMap(string mapFile)
        {
            using (BinaryReader binary = new BinaryReader(new FileStream(mapFile, FileMode.Open)))
            {
                // There is still much of this file I have not figured out.

                // Header - 30 bytes. We only know 10 of them.
                var mapWidth = binary.ReadInt16();
                var mapHeight = binary.ReadInt16();

                var x = binary.ReadInt16();
                var y = binary.ReadInt16();
                var z = binary.ReadInt16();

                if (z != 0)
                    return; // Not dealing with mines yet.

                // The rest of the header is unknown.
                binary.ReadBytes(20); // Just skip.

                var map = new Map(x, y, z, mapWidth, mapHeight);

                // Next up is the height map.

                for (int i = 0; i < mapHeight; i++)
                {
                    for (int j = 0; j < mapWidth; j++)
                    {
                        map.SetElevation(j, i, binary.ReadByte());
                    }
                }

                // var waterMap = new byte[mapHeight, mapWidth];

                for (int i = 0; i < mapHeight; i++)
                {
                    for (int j = 0; j < mapWidth; j++)
                    {
                        map.SetWater(j, i, binary.ReadByte());
                    }
                }

                // Land ownership is next. Each Entry is 0xD6 (214) bytes long.
                // Except, Mickey seperates every 10 with a blank entry.

                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 11; j++)
                    {
                        binary.ReadBytes(0xD6); // Skipping these for now. // TODO ::  
                    }
                }

                // The next bytes are unknown, but I believe its 0x933 (2355) bytes in length.

                binary.ReadBytes(0x932); // TODO :: Verify this spacing.

                // Next is the surface map.
                var p = binary.BaseStream.Position;

                for (int i = 0; i < mapHeight; i++)
                {
                    for (int j = 0; j < mapWidth; j++)
                    {
                        var b = binary.ReadByte();
                        map.SetSurface(j, i, b);
                    }
                }

                // And lastly, 0x855 (2128) bytes remain. Not sure on these either.
                World.World.AddMap(map);
            }
        }

    }
}
