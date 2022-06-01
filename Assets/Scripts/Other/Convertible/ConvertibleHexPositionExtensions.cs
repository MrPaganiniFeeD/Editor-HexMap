using System;
using System.Security.Cryptography;
using System.Threading;
using Model;
using UnityEngine;

namespace Other.Convertible
{
    public static class ConvertPositionExtensions
    {
        public static float XOffset => _xOffset;
        public static float YOffset => _yOffset;
        public static float ZOffset => _zOffset;
        
        public const float _xOffset = 1.991858f;
        public const float _yOffset = 1;
        public const float _zOffset = 1.725f;

        private const float _outerRadius = 1.15f;
        private const float _innerRadius = _outerRadius * 0.866025404f;
        
        public static Vector3 GetConvertiblePosition(this HexCoordinates coordinates)
        {
            var position = new Vector3();
            position.x = (coordinates.x + coordinates.z / 2 + coordinates.z * 0.5f - coordinates.z / 2) * (_innerRadius * 2f);
            position.z = coordinates.z * (_outerRadius * 1.5f);
            return position;
        }
        public static HexCoordinates GetConvertibleCoordinates(this Vector3 position)
        {
            var x = Mathf.RoundToInt((position.x - position.z / 1.74f) / (_innerRadius * 2f));
            var y = 0;    
            var z = Mathf.RoundToInt(position.z / (_outerRadius * 1.5f));
            return new HexCoordinates(x, y, z);
        } 
    }
}