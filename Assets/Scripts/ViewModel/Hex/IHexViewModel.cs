using System;
using Model.HexModel;
using Model.HexModel.TerrainHex;
using Model.Rotation;
using UniRx;
using UnityEngine;

namespace ViewModel.HexViewModel
{
    public interface IHexViewModel : IViewModel, IDisposable
    {
        event Action Delete;
        Vector3 Position { get; }

        HexBiomeType HexBiomeType { get; }
        ReactiveProperty<RotationLocal> GetRotation();
        ReactiveProperty<HexBiomeType> GetHexType();
        ReactiveProperty<ITerrainViewModel> GetTerrain();
        int GetNumberOfNeighbour();
    }
}