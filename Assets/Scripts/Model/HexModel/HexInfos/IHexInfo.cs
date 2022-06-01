using System.Collections.Generic;
using Model.HexModel.HexInfos;
using Model.Rotation;
using UnityEngine;

namespace Model.HexModel
{
    public interface IHexInfo
    {
        HexBiomeType BiomeType { get; }
        public TerrainHexType TerrainHexType { get; }
        List<TerrainHexType> AccessibleTerrain { get; }
        float HeightSpawn { get; }
        float ChanceSpawn { get; }
    }
}