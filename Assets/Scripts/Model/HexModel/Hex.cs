using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Model.HexModel.TerrainHex;
using Model.Rotation;
using Model.Transformable;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Model.HexModel
{
    public class Hex : IModel, ITransformable<HexCoordinates>, IHex
    {
        public event Action<HexCoordinates> ChangePosition;
        public event Action<RotationLocal> ChangeRotation;
        public event Action<HexBiomeType> ChangeHexType;
        public event Action Updated;
        public event Action DeleteHex;

        public HexCoordinates Position
        {
            get => _coordinates;
            set
            {
                _coordinates = value;
                ChangePosition?.Invoke(_coordinates);
            }
        }

        public RotationLocal Rotation
        {
            get => _rotation;
            set
            {
                _rotation = value;
                ChangeRotation?.Invoke(_rotation);
            }

        }

        public HexBiomeType BiomeType => _biomeType;
        public ITerrain Terrain => _terrain;
        public Hex[] Neighbours = new Hex[6];


        private HexCoordinates _coordinates;
        private RotationLocal _rotation;
        private HexBiomeType _biomeType;
        private ITerrain _terrain;

        public Hex(HexCoordinates hexCoordinates, RotationLocal rotation, HexBiomeType biomeType)
        {
            Position = hexCoordinates;
            _rotation = rotation;
            _biomeType = biomeType;
        }

        public void SwitchBiomeType(HexBiomeType hexBiomeType)
        {
            _biomeType = hexBiomeType;
            ChangeHexType?.Invoke(_biomeType);
        }

        public void SetTerrain(ITerrain terrain)
        {
            foreach (var biomeType in terrain.BiomesOnWhichYouCanPlace)
            {
                if (_biomeType == biomeType)
                {
                    _terrain = terrain;
                    _terrain.Refresh();
                    Updated?.Invoke();
                }
            }
        }

        public void DeleteTerrain()
        {
            //Delete;
            _terrain = null;
        }

        public void SetNeighbors(HexDirection hexDirection, Hex hex)
        {
            Neighbours[(int)hexDirection] = hex;
            hex.Neighbours[(int)hexDirection.Opposite()] = this;
        }

        public Hex GetNeighbour(HexDirection direction)
        {
            return Neighbours[(int)direction];
        }
        public void Refresh()
        {
            Debug.Log("Refresh hex");
        }
        public void Delete()
        {
            DeleteHex?.Invoke();
        }
    }
}