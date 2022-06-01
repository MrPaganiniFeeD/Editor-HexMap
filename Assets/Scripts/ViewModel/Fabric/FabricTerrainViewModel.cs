using Model.HexModel;
using Model.HexModel.TerrainHex;
using ViewModel.HexViewModel;

namespace ViewModel.Fabric
{
    public class FabricTerrainViewModel : IFabricHexViewModel<ITerrainViewModel>
    {
        public ITerrainViewModel CreationHexViewModel(Hex hex)
        {
            if (hex.Terrain is RoadTerrain road)
                return new RoadTerrainViewModel(hex, road);
            return null;
        }
    }
}