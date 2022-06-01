using System.Collections.Generic;
using Model.Rotation;
using UnityEngine;

namespace Model.HexModel.HexInfos
{
    public class HexSandInfo : IHexInfo
    {
        public HexBiomeType BiomeType => HexBiomeType.Sand;
        public TerrainHexType TerrainHexType => _terrainHexType; 
        public List<TerrainHexType> AccessibleTerrain => _accessibleTerrain;
        public float HeightSpawn => 0.55f;
        public float ChanceSpawn => 1f;
        
        private List<TerrainHexType> _accessibleTerrain = new List<TerrainHexType>()
        {
            TerrainHexType.Rock,
            TerrainHexType.Forest,
            TerrainHexType.River
        };
        private TerrainHexType _terrainHexType;

        public HexSandInfo(TerrainHexType terrainHexType)
        {
            _terrainHexType = terrainHexType;
        }
    }
}