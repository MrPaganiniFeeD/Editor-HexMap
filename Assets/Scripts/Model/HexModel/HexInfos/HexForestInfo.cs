using System.Collections.Generic;
using TMPro;

namespace Model.HexModel.HexInfos
{
    public class HexForestInfo : IHexInfo
    {
        public HexBiomeType BiomeType => HexBiomeType.Forest;
        public TerrainHexType TerrainHexType => _terrainHexType;
        public List<TerrainHexType> AccessibleTerrain => _accessibleTerrain;
        public float HeightSpawn => 0.8f;
        public float ChanceSpawn => 1f;

        private List<TerrainHexType> _accessibleTerrain = new List<TerrainHexType>()
        {
            TerrainHexType.Rock,
            TerrainHexType.Forest,
            TerrainHexType.River
        };

        private TerrainHexType _terrainHexType;
        public HexForestInfo(TerrainHexType terrainHexType)
        {
            _terrainHexType = terrainHexType;
        }
    }
}