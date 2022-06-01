using System.Collections.Generic;
using Model.Rotation;
using TMPro;
using UnityEngine;

namespace Model.HexModel.HexInfos
{
    public class HexSeaInfo : IHexInfo
    {
        public HexBiomeType BiomeType => HexBiomeType.Sea;
        public TerrainHexType TerrainHexType => _terrainHexType;
        public List<TerrainHexType> AccessibleTerrain => _accessibleTerrain;
        public float HeightSpawn => 0.4f;
        public float ChanceSpawn => 1f;
        private List<TerrainHexType> _accessibleTerrain = new List<TerrainHexType>()
        {
            TerrainHexType.Rock,
            TerrainHexType.Forest,
            TerrainHexType.River
        };

        private TerrainHexType _terrainHexType;
        public HexSeaInfo(TerrainHexType terrainHexType)
        {
            _terrainHexType = terrainHexType;
        }
    }
}       