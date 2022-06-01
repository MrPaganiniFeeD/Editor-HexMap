using System;
using Model.HexModel;
using Model.Rotation;

namespace Model.Data
{
    [Serializable]
    public struct HexData
    {
        public HexCoordinates Coordinates;
        public HexBiomeType BiomeType;
        public RotationLocal Rotation;
    }
}