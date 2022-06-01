using Model.HexModel;
using UnityEngine;
using View.HexViews;
using ViewModel.HexViewModel;

namespace View.Fabric
{
    public interface IFabricHexPrefabs
    {
        IHexView CreationHexView(IHexViewModel hexViewModel);
    }
}