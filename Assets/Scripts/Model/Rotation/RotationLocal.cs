using System;

namespace Model.Rotation
{
    [Serializable]
    public struct RotationLocal
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }


        public RotationLocal(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}