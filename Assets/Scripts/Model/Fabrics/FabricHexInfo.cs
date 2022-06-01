using System;
using Model.HexModel;
using Model.HexModel.HexInfos;

namespace Model.Fabrics
{
    public class FabricHexInfo
    {
        public IHexInfo CreateHexInfo(HexBiomeType hexBiomeType)
        {
            return hexBiomeType switch
            {
                HexBiomeType.Forest => new HexForestInfo(TerrainHexType.None),
                HexBiomeType.Sand => new HexSandInfo(TerrainHexType.None),
                HexBiomeType.ShallowSea => new HexShallowSeaInfo(TerrainHexType.None),
                HexBiomeType.Sea => new HexSeaInfo(TerrainHexType.None),
                _ => throw new ArgumentOutOfRangeException(nameof(hexBiomeType), hexBiomeType, null)
            };
        }
    }
}