using System;
using System.Collections.Generic;
using Model.HexModel;
using UnityEngine;
using View.HexViews;
using ViewModel.HexViewModel;

namespace ViewModel.HexGridVM
{
    public interface IHexGridViewModel : IViewModel, IDisposable
    {
        event Action<List<IHexViewModel>> CreatedHexGrid;
        event Action<IHexViewModel> AddedHex;
        event Action<IHexViewModel> RemovedHex;

        public bool TryAddHex(Vector3 position, IHexInfo hexInfo);
        public Vector3 GetNeighbourPosition(Vector3 position, HexDirection direction);
        public IEnumerable<Vector3> GetNeighboursPosition(Vector3 hexPosition);
        void Save();
        void Load();
    }
    
}