using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpgwo_Server.World
{
    public class Map
    {
        private readonly int _x;
        public int X => _x;

        private readonly int _y;
        public int Y => _y;

        private readonly int _z;
        public int Z => _z;

        private readonly int _mapWidth;
        public int Width => _mapWidth;

        private readonly int _mapHeight;
        public int Height => _mapHeight;

        private byte[,] _heightMap;
        private byte[,] _waterMap;
        private byte[,] _surfaceMap;

        // Grass will need it's own system at some point, since it will be dynamic.
        // Players walking upon grass should degrade, and server ticks shoulder grow.
        // Admins should not effect grass either.
        // For now, I'll just use a byte. Each step upon grass can subtract, then mod 10 to determine stage.
        private byte[,] _grassMap;

        // RPGWO Maps consist of multiple parts.
        // Height Map.
        // Water Map.
        // Surface Map.
        // Landownership.
        // 0-9 -> Grass, lines up for 10 grass types.
        // Server needs to calculate the proper tile image for the tile to render.

        public Map(int x, int y, int z, int mapWidth, int mapHeight)
        {
            _x = x;
            _y = y;
            _z = z;
            _mapWidth = mapWidth;
            _mapHeight = mapHeight;

            // Initialize maps.
            _heightMap = new byte[_mapWidth, _mapHeight];
            _waterMap = new byte[_mapWidth, _mapHeight];
            _surfaceMap = new byte[_mapWidth, _mapHeight];
            _grassMap = new byte[_mapHeight, _mapHeight];
        }

        public byte GetElevation(int x, int y)
        {
            if (x >= 0 && x < _mapWidth && y >= 0 && y < _mapHeight)
            {
                return _heightMap[x, y];
            }

            return 0;
        }

        public void SetElevation(int x, int y, byte height)
        {
            if (x >= 0 && x < _mapWidth && y >= 0 && y < _mapHeight)
            {
                _heightMap[x, y] = height;
            }
        }

        public void SetWater(int x, int y, byte water)
        {
            if (x >= 0 && x < _mapWidth && y >= 0 && y < _mapHeight)
            {
                _waterMap[x, y] = water;
            }
        }

        public byte GetSurface(int x, int y)
        {
            if (x >= 0 && x < _mapWidth && y >= 0 && y < _mapHeight)
            {
                return _surfaceMap[x, y];
            }

            return 0;
        }

        public void SetSurface(int x, int y, byte surface)
        {
            if (x >= 0 && x < _mapWidth && y >= 0 && y < _mapHeight)
            {
                _surfaceMap[x, y] = surface;
            }
        }
    }
}
