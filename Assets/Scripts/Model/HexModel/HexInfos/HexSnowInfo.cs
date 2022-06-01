using System.Collections.Generic;

namespace Model.HexModel.HexInfos
{
    public class HexSnowInfo : IHexInfo
    {
        public HexBiomeType BiomeType => HexBiomeType.Snow;
        public TerrainHexType TerrainHexType => _terrainHexType;
        public List<TerrainHexType> AccessibleTerrain => _accessibleTerrain;
        public float HeightSpawn => 1.1f;
        public float ChanceSpawn => 1f;

        private List<TerrainHexType> _accessibleTerrain = new List<TerrainHexType>()
        {
            TerrainHexType.Rock,
            TerrainHexType.Forest,
            TerrainHexType.River
        };

        private TerrainHexType _terrainHexType;
        public HexSnowInfo(TerrainHexType terrainHexType)
        {
            _terrainHexType = terrainHexType;
        }
    }
}