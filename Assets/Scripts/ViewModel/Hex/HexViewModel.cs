using System;
using System.Linq;
using Model.HexModel;
using Model.Rotation;
using Other.Convertible;
using UniRx;
using UnityEngine;
using ViewModel.Fabric;

namespace ViewModel.HexViewModel
{
    public class HexViewModel : IHexViewModel
    {
        public event Action Delete;
        public Vector3 Position { get; }
        public int Neighbour => _hex.Neighbours.Length;
        public HexBiomeType HexBiomeType => _hex.BiomeType;

        private Hex _hex;
        private IFabricHexViewModel<ITerrainViewModel> _fabricTerrainViewModel;

        private ReactiveProperty<RotationLocal> _changedRotation = new ReactiveProperty<RotationLocal>();
        private ReactiveProperty<HexBiomeType> _changedHexType = new ReactiveProperty<HexBiomeType>();
        private ReactiveProperty<ITerrainViewModel> _changedTerrain = new ReactiveProperty<ITerrainViewModel>();

        public HexViewModel(Hex hex)
        {
            _hex = hex;
            _fabricTerrainViewModel = new FabricTerrainViewModel();
            _changedHexType.Value = _hex.BiomeType;
            Position = _hex.Position.GetConvertiblePosition();
            _hex.ChangeHexType += OnChangeHexType;
            _hex.ChangeRotation += OnChangeRotation;
            _hex.Updated += OnChangeTerrain;
            _hex.DeleteHex += OnDelete;
        }

        private void OnDelete() => 
            Delete?.Invoke();

        private void OnChangeTerrain() => 
            _changedTerrain.Value = _fabricTerrainViewModel.CreationHexViewModel(_hex);

        private void OnChangeRotation(RotationLocal rotation) => 
            _changedRotation.Value = rotation;

        private void OnChangeHexType(HexBiomeType hexType) => 
            _changedHexType.Value = hexType;

        public ReactiveProperty<RotationLocal> GetRotation() => 
            _changedRotation;

        public ReactiveProperty<HexBiomeType> GetHexType() => 
            _changedHexType;

        public ReactiveProperty<ITerrainViewModel> GetTerrain() => 
            _changedTerrain;

        public int GetNumberOfNeighbour() => 
            _hex.Neighbours.Count(neighbour => neighbour != null);


        public void Dispose()
        {
            _hex.ChangeHexType -= OnChangeHexType;
            _hex.ChangeRotation -= OnChangeRotation;
            _hex.Updated -= OnChangeTerrain;
            _hex.DeleteHex -= OnDelete;
        }
    }
}