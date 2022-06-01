using Model.HexModel;
using ViewModel.Fabric;
using ViewModel.HexViewModel;

namespace Model.Fabrics
{
    public class FabricHexViewModel : IFabricHexViewModel<IHexViewModel> 
    {
        public IHexViewModel CreationHexViewModel(Hex hex) => 
            new HexViewModel(hex);
    }
}