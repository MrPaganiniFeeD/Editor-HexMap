using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.HexModel.TerrainHex
{
    public class RoadTerrain : IModel, ITerrain
    {
        public event Action Refreshed;
        
        public List<HexBiomeType> BiomesOnWhichYouCanPlace => _biomesOnWichYouCanPlace;
        public int NumberOfNeighbourRoad => _hexNeighboursDirections.Count;
        public List<HexDirection> HexNeighboursDirections => _hexNeighboursDirections;

        private HexDirection _incomingRoadDirection;
        private HexDirection? _outgoingRoadDirection;
        private List<HexDirection> _hexNeighboursDirections;
        private bool _hasIncomingRoad, _hasOutgoingRoad;
        private Hex _hex;


        private List<HexBiomeType> _biomesOnWichYouCanPlace = new List<HexBiomeType>()
        {
            HexBiomeType.Forest,
            HexBiomeType.Sand
        };

        public RoadTerrain(Hex hex)
        {
            _hex = hex;
            _hexNeighboursDirections = new List<HexDirection>();
            _outgoingRoadDirection = null;
        }

        public RoadTerrain SetRoad(HexDirection direction)
        {
            if (_outgoingRoadDirection == direction)
            {
                return null;
            }
            
            Hex neighbour = _hex.GetNeighbour(direction);
            _outgoingRoadDirection = direction;
            UpdateStateNeighboringRoads();
            if (neighbour.Terrain is RoadTerrain == false)
            {
                var neighbourRoad = new RoadTerrain(neighbour);
                neighbour.SetTerrain(neighbourRoad);
                neighbourRoad.UpdateNeighboringRoads();
            }
            UpdateNeighboringRoads();

            return this;
        }

        private void UpdateStateNeighboringRoads()
        {
            var neighboringRoads = GetNeighboringRoads();
            foreach (var road in neighboringRoads)
                road.UpdateNeighboringRoads();
        }

        private void UpdateNeighboringRoads()
        {
            _hexNeighboursDirections.Clear();
            for (HexDirection direction = HexDirection.NE; direction <= HexDirection.NW; direction++)
            {
                Hex neighbour = _hex.GetNeighbour(direction);
                if (neighbour.Terrain is RoadTerrain)
                    _hexNeighboursDirections.Add(direction);
            }
            Debug.Log(_hexNeighboursDirections.Count);
            Refreshed?.Invoke();
        }

        private List<RoadTerrain> GetNeighboringRoads()
        {
            List<RoadTerrain> neighboringRoads = new List<RoadTerrain>();
            foreach (Hex neighbour in _hex.Neighbours)
                if(neighbour.Terrain is RoadTerrain road)
                   neighboringRoads.Add(road);
            
            return neighboringRoads;
        }
        private void RemoveRoad() =>
            Debug.Log("RemovedRoad");

        public void Delete() => 
            throw new NotImplementedException();

        public void Refresh()
        {
            //UpdateNeighboringRoads();
            Refreshed?.Invoke();
        }
    }
}