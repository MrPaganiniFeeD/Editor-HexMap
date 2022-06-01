using UnityEngine;
using ViewModel.HexViewModel;

namespace View.HexViews
{
    public interface IHexView : IView
    {
        Vector3 Position { get; }

        void Init(IHexViewModel hexViewModel);
        void Delete();
    }
}