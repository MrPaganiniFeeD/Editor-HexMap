using System;
using System.Collections.Generic;
using Model;
using Model.HexModel;
using Other.Convertible;
using UnityEngine;


namespace DefaultNamespace.TemplateCommand.Command
{
    public class SwitchHexTypeCommand : ITemplateCommand
    {
        private HexBiomeType _hexBiomeType;
        private HexGrid _hexGrid;
        private int _brushSize;
        private Hex _selectionHex;
        private Dictionary<Hex, HexBiomeType> _modifiedHexes;

        public SwitchHexTypeCommand(HexBiomeType hexBiomeType, int brushSize, HexGrid hexGrid)
        {
            _hexBiomeType = hexBiomeType;
            _hexGrid = hexGrid;
            _brushSize = brushSize;
            _modifiedHexes = new Dictionary<Hex, HexBiomeType>();
        }

        public void Execute()
        {
            //EditHexes(_hex.Position);
        }

        public void UpdateNextHex(Hex hex)
        {
             EditHex(hex.Position);
        }

        private void EditHex (HexCoordinates center) 
        {
            var centerX = center.x;
            var centerZ = center.z;

            for (int r = 0, z = centerZ - _brushSize; z <= centerZ; z++, r++)
                for (int x = centerX - r; x <= centerX + _brushSize; x++)
                    ChangeTypeHex(new HexCoordinates(x, 0, z));

            for (int r = 0, z = centerZ + _brushSize; z > centerZ; z--, r++)
                for (int x = centerX - _brushSize; x <= centerX + r; x++)
                    ChangeTypeHex(new HexCoordinates(x, 0, z));
                    
        }

        private void ChangeTypeHex(HexCoordinates coordinates)
        {
            var hex = _hexGrid.GetHex(coordinates);
            if(hex == null)
                return;
            if(hex.BiomeType == _hexBiomeType)
                return;
            _modifiedHexes.Add(hex, hex.BiomeType);
            hex.SwitchBiomeType(_hexBiomeType);
        }

        public void Undo()
        {
            foreach (var key in _modifiedHexes)
            {
                var hex = key.Key;
                hex.SwitchBiomeType(key.Value);
            }
        }
    }
}