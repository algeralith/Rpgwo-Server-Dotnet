using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpgwo_Server.World
{
    public class World
    {
        // TODO :: Seriously, I could use a less convoluted structure.
        private static Dictionary<int, Dictionary<int, Dictionary<int, Map>>> _maps = new Dictionary<int, Dictionary<int, Dictionary<int, Map>>>();

        public static void AddMap(Map map)
        {
            if (!_maps.ContainsKey(map.X))
                _maps[map.X] = new Dictionary<int, Dictionary<int, Map>>();

            if (!_maps[map.X].ContainsKey(map.Y))
                _maps[map.X][map.Y] = new Dictionary<int, Map>();

            _maps[map.X][map.Y][map.Z] = map;
        }

        // TODO :: This can be written better. Just copy pasted it from old code for now.
        public static Int16[] GetMapLineData(int x, int y, CardinalDirection direction)
        {
            Int16[] tiles;

            if (direction == CardinalDirection.North || direction == CardinalDirection.South)
                tiles = new Int16[19];
            else
                tiles = new Int16[17];

            switch(direction)
            {
                case CardinalDirection.North:
                    {
                        var startX = x - 9;
                        var startY = y - 8;

                        for (int i = 0; i < tiles.Length; i++)
                        {
                            tiles[i] = GetTile(startX + i, startY, 0);
                        }
                    }
                    break;

                case CardinalDirection.South:
                    {
                        var startX = x - 9;
                        var startY = y + 8;

                        for (int i = 0; i < tiles.Length; i++)
                        {
                            tiles[i] = GetTile(startX + i, startY, 0);
                        }
                    }
                    break;

                case CardinalDirection.East:
                    {
                        var startX = x + 9;
                        var startY = y - 8;

                        for (int i = 0; i < tiles.Length; i++)
                        {
                            tiles[i] = GetTile(startX, startY + i, 0);
                        }
                    }
                    break;

                case CardinalDirection.West:
                    {
                        var startX = x - 9;
                        var startY = y - 8;

                        for (int i = 0; i < tiles.Length; i++)
                        {
                            tiles[i] = GetTile(startX, startY + i, 0);
                        }
                    }
                    break;
            }

            return tiles;
        }

        public static Int16[] GetViewport(int x, int y, int z)
        {
            // TODO -- THIS ENTIRE FUCKING THING.
            var tiles = new Int16[19 * 17];

            // For now, let's just asssume the character is in the middle of the rect.
            var index = 0;
            for (int j = x - (19 / 2); j <= x + (19 / 2); j++)
            { 
                for (int i = y - (17 / 2); i <= y + (17 / 2); i++)
                {
                    tiles[index] = GetTile(j, i, 0);
                    index++;
                }
            }

            return tiles;
        }

        public static Int16 GetElevationTile(int x, int y, int z)
        {
            /*
             * This is all a test for now. I believe elevation tiles are determined by the bottom, right, and bottom right tiles.
             * 
             * At some point in the future, I can swap this over to some bitwise logic.
             */

            //
            var elevation = GetElevation(x, y, z);
            var bottom = elevation - GetElevation(x, y + 1, z);
            var right = elevation - GetElevation(x + 1, y, z);
            var bottomRight = elevation - GetElevation(x + 1, y + 1, z);

            var imageTile = 0;

            switch(bottom)
            {
                case -1:
                    switch(right)
                    {
                        case -1:
                            switch (bottomRight)
                            {
                                case -1:
                                    imageTile = 19;
                                    break;
                                case 0:
                                    imageTile = 15;
                                    break;
                                case 1:
                                    break;
                            }
                            break;
                        case 0:
                            switch (bottomRight)
                            {
                                case -1:
                                    imageTile = 17;
                                    break;
                                case 0:
                                    imageTile = 13;
                                    break;
                                case 1:
                                    break;
                            }
                            break;
                        case 1:
                            switch (bottomRight)
                            {
                                case -1:
                                    break;
                                case 0:
                                    break;
                                case 1:
                                    break;
                            }
                            break;
                    }
                    break;

                case 0:
                    switch (right)
                    {
                        case -1:
                            switch (bottomRight)
                            {
                                case -1:
                                    imageTile = 11;
                                    break;
                                case 0:
                                    imageTile = 7;
                                    break;
                                case 1:
                                    break;
                            }
                            break;
                        case 0:
                            switch (bottomRight)
                            {
                                case -1:
                                    imageTile = 9;
                                    break;
                                case 0:
                                    break;
                                case 1:
                                    imageTile = 16;
                                    break;
                            }
                            break;
                        case 1:
                            switch (bottomRight)
                            {
                                case -1:
                                    break;
                                case 0:
                                    imageTile = 18;
                                    break;
                                case 1:
                                    imageTile = 14;
                                    break;
                            }
                            break;
                    }
                    break;

                case 1:
                    switch (right)
                    {
                        case -1:
                            switch (bottomRight)
                            {
                                case -1:
                                    break;
                                case 0:
                                    break;
                                case 1:
                                    break;
                            }
                            break;
                        case 0:
                            switch (bottomRight)
                            {
                                case -1:
                                    break;
                                case 0:
                                    imageTile = 12;
                                    break;
                                case 1:
                                    imageTile = 8;
                                    break;
                            }
                            break;
                        case 1:
                            switch (bottomRight)
                            {
                                case -1:
                                    break;
                                case 0:
                                    imageTile = 10;
                                    break;
                                case 1:
                                    imageTile = 6;
                                    break;
                            }
                            break;
                    }
                    break;
            }

            return (Int16)(100 + imageTile);
        }

        public static byte GetElevation(int x, int y, int z)
        {
            int mapX = (x / 200) * 200;
            int mapY = (y / 200) * 200;

            if (_maps.ContainsKey(mapX))
            {
                if (_maps[mapX].ContainsKey(mapY))
                {
                    if (_maps[mapX][mapY].ContainsKey(z))
                    {
                        var map = _maps[mapX][mapY][z];

                        return map.GetElevation(x - mapX, y - mapY);
                    }
                }
            }

            return 0;
        }

        public static Int16 GetTile(int x, int y, int z)
        {
            // 000x000 contains 0 -> 199.
            // Thus, mapX = (x / 200) * 200
            int mapX = (x / 200) * 200;
            int mapY = (y / 200) * 200;

            if (_maps.ContainsKey(mapX))
            {
                if (_maps[mapX].ContainsKey(mapY))
                {
                    if (_maps[mapX][mapY].ContainsKey(z))
                    {
                        var map = _maps[mapX][mapY][z];

                        int tile = map.GetSurface(x - mapX, y - mapY);

                        if (tile >= 100 && tile <= 199)
                            tile -= 100;

                        return (Int16)tile;
                        //  return GetElevationTile(x, y, z);
                    }
                }
            }

            return 0;
        }
    }
}
