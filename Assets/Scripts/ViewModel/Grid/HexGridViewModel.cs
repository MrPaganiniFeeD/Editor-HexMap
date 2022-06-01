using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using Model.Fabrics;
using Model.HexModel;
using Model.Rotation;
using Other.Convertible;
using UnityEngine;
using ViewModel.Fabric;
using ViewModel.HexViewModel;

namespace ViewModel.HexGridVM
{
    public class HexGridViewModel : IHexGridViewModel
    {
        public event Action<List<IHexViewModel>> CreatedHexGrid;
        public event Action<IHexViewModel> AddedHex;
        public event Action<IHexViewModel> RemovedHex;

        private HexGrid _hexGrid;
        private IFabricHex _fabricHex;
        private IFabricHexViewModel<IHexViewModel> _fabricHexViewModel;
        private List<HexDirection> _allHexDirections = new List<HexDirection>()
        {
            HexDirection.NE,
            HexDirection.E,
            HexDirection.SE,
            HexDirection.SW,
            HexDirection.W,
            HexDirection.NW
        };


        public HexGridViewModel(HexGrid hexGrid, IFabricHex fabricHex, IFabricHexViewModel<IHexViewModel> fabricHexViewModel)
        {
            _hexGrid = hexGrid;
            _fabricHex = fabricHex;
            _fabricHexViewModel = fabricHexViewModel;
            _hexGrid.CreatedHexGrid += OnCreatedHexGrid;
            _hexGrid.AddedHex += OnAddedHex;
            _hexGrid.RemovedHex += OnRemovedHex;
        }

        private void OnCreatedHexGrid(List<Hex> hexGird)
        {
            Debug.Log("OnCreatedHexGrid");
            List<IHexViewModel> updatedGrid = new List<IHexViewModel>();
            foreach (Hex hex in hexGird)
                updatedGrid.Add(_fabricHexViewModel.CreationHexViewModel(hex));

            CreatedHexGrid?.Invoke(updatedGrid);
        }

        private void OnAddedHex(Hex hex)
        {
            IHexViewModel addHexViewModel = _fabricHexViewModel.CreationHexViewModel(hex);
            AddedHex?.Invoke(addHexViewModel);
        }

        private void OnRemovedHex(Hex hex)
        {
            IHexViewModel removedHexViewModel = _fabricHexViewModel.CreationHexViewModel(hex);
            RemovedHex?.Invoke(removedHexViewModel);
        }

        public bool TryAddHex(Vector3 position, IHexInfo hexInfo)
        {
            HexCoordinates coordinates = position.GetConvertibleCoordinates();
            Hex hex = _fabricHex.CreateHex(coordinates, new RotationLocal(), hexInfo.BiomeType, false);
            return _hexGrid.TryAddHex(hex);
        }

        public void Save() => 
            _hexGrid.Save();

        public void Load() => 
            _hexGrid.Load();

        public Vector3 GetNeighbourPosition(Vector3 position, HexDirection direction)
        {
            HexCoordinates coordinates = position.GetConvertibleCoordinates();
            Hex hex = _hexGrid.GetHex(coordinates);
            Hex hexNeighbor = hex.Neighbours[(int) direction];
            return hexNeighbor.Position.GetConvertiblePosition();
        }

        public IEnumerable<Vector3> GetNeighboursPosition(Vector3 hexPosition) => 
            _allHexDirections.Select(direction => GetNeighbourPosition(hexPosition, direction)).ToList();

        public void Dispose()
        {
            _hexGrid.CreatedHexGrid -= OnCreatedHexGrid;
            _hexGrid.AddedHex -= OnAddedHex;
            _hexGrid.RemovedHex -= OnRemovedHex;
        }
    }
}