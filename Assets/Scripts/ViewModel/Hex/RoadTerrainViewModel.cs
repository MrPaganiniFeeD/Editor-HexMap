using System;
using Model.HexModel;
using Model.HexModel.TerrainHex;
using UnityEngine;

namespace ViewModel.HexViewModel
{
    public class RoadTerrainViewModel : ITerrainViewModel, IDisposable
    {
        public event Action RefreshedValue;

        public int NumberOfNeighbourRoad => _numberOfNeghbourRoad;
        public HexBiomeType BiomeTypeRoad => _biomeTypeRoad;


        private HexBiomeType _biomeTypeRoad;
        private int _numberOfNeghbourRoad;
        private bool _hasIncomingRoad;
        private bool _hasOutgoingRoad;
        private RoadTerrain _road;
        private Hex _hex;

        public RoadTerrainViewModel(Hex hex, RoadTerrain road)
        {
            _hex = hex;
            _road = road;
            OnRoadRefreshed();
            _road.Refreshed += OnRoadRefreshed;
        }
        private void OnRoadRefreshed()
        {
            _numberOfNeghbourRoad = _road.NumberOfNeighbourRoad;
            _biomeTypeRoad = _hex.BiomeType;
            RefreshedValue?.Invoke();
        }
        
        public void Dispose() => 
            _road.Refreshed -= OnRoadRefreshed;
    }
}