using System.Collections.Generic;
using Model.Rotation;
using UnityEngine;

namespace Model.HexModel.HexInfos
{
    public class HexShallowSeaInfo : IHexInfo
    {
        public HexBiomeType BiomeType => HexBiomeType.ShallowSea;
        public TerrainHexType TerrainHexType => _terrainHexType;
        public List<TerrainHexType> AccessibleTerrain => _accessibleTerrain;
        public float HeightSpawn => 0.45f;
        public float ChanceSpawn => 1f;

        private TerrainHexType _terrainHexType;

        private List<TerrainHexType> _accessibleTerrain = new List<TerrainHexType>()
        {
            TerrainHexType.Rock,
            TerrainHexType.Forest,
            TerrainHexType.River
        };
        public HexShallowSeaInfo(TerrainHexType terrainHexType)
        {
            _terrainHexType = terrainHexType;
        }
    }
}