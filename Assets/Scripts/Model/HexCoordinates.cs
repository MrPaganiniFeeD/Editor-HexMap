using System;
using UnityEngine;

namespace Model
{
    [Serializable]
    public struct HexCoordinates
    { 
        public int x;
        public int y;
        public int z;

        public HexCoordinates(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        
        public static HexCoordinates operator +(HexCoordinates a, HexCoordinates b)
        {
            return new HexCoordinates()
            {
                x = a.x + b.x,
                y = a.y + b.y,
                z = a.z + b.z,
            };
        }
        public static HexCoordinates operator +(HexCoordinates a, Vector3 b)
        {
            return new HexCoordinates()
            {
                x = a.x + (int)b.x,
                y = a.y + (int)b.y,
                z = a.z + (int)b.z,
            };
        }

        public static bool operator ==(HexCoordinates a, HexCoordinates b)
        {
            return a.x == b.x && a.y == b.y && a.z == b.z;
        }

        public static bool operator !=(HexCoordinates a, HexCoordinates b)
        {
            return a.x != b.x && a.y != b.y && a.z != b.z;
        }

    }
}