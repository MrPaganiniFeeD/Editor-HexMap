using Model.HexModel;
using ViewModel.HexViewModel;

namespace ViewModel.Fabric
{
    public interface IFabricHexViewModel<out T>
    {
        public T CreationHexViewModel(Hex hex);
    }
}