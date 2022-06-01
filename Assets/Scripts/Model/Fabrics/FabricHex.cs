using System;
using System.Collections.Generic;
using System.Linq;
using Model.HexModel;
using Model.Rotation;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Zenject;

namespace Model.Fabrics
{
    public class FabricHex : IFabricHex
    {
        private HexGrid _hexGrid;

        public FabricHex(HexGrid hexGrid)
        {
            _hexGrid = hexGrid;
        }

        public Hex CreateHex(HexCoordinates coordinates, RotationLocal rotation, HexBiomeType hexBiomeType,
            bool isAddToGrid)
        {
            var hex = new Hex(coordinates, rotation, hexBiomeType);


            if (coordinates.x + coordinates.z / 2 > 0)
                SetNeighbors(hex, HexDirection.W, new HexCoordinates(coordinates.x - 1, 0, coordinates.z));
            if (coordinates.z > 0)
            {
                if ((coordinates.z & 1) == 0)
                {
                    SetNeighbors(hex, HexDirection.SE, new HexCoordinates(coordinates.x + 1, 0, coordinates.z - 1));
                    if ((coordinates.x + coordinates.z / 2) > 0)
                    {
                        SetNeighbors(hex, HexDirection.SW, new HexCoordinates(coordinates.x, 0, coordinates.z - 1));
                 
                    }
                }
                else
                {
                    SetNeighbors(hex, HexDirection.SW, new HexCoordinates(coordinates.x, 0, coordinates.z - 1));
                    if (coordinates.x < coordinates.x + _hexGrid.Width - 1 - coordinates.x - coordinates.z / 2)
                    {
                        SetNeighbors(hex, HexDirection.SE, new HexCoordinates(coordinates.x + 1, 0, coordinates.z - 1));
                    }
                }
            }

            if (isAddToGrid == false)
                return hex;
            _hexGrid.AddHex(hex);
            int countNeighbour;

            return hex;
        }

        private void SetNeighbors(Hex hex, HexDirection direction, HexCoordinates coordinatesNeighbors)
        {
            hex.SetNeighbors(direction, _hexGrid.GetHex(coordinatesNeighbors));
            var count = _hexGrid.GetHex(coordinatesNeighbors).Neighbours.Count(hex1 => hex1 != null);
        }

        public Hex CreateHex(IHexInfo hexInfo)
        {
            return new Hex(new HexCoordinates(0, 0, 0), new RotationLocal(), hexInfo.BiomeType);
        }
    }
}