using System;
using CreatingMapMode.View;
using DefaultNamespace;
using DefaultNamespace.TemplateCommand.Command;
using Model;
using Model.HexModel;
using Model.HexModel.TerrainHex;
using UnityEngine;

namespace CreatingMapMode.Model
{
    public class HexEditor : IModel, IDisposable
    {
        public event Action UpdateValues;

        public int BrushSize => _brushSize;
        public HexBiomeType SelectedHexBiomeType => _selectedHexBiomeType;
        public OptionalToggle RiverMode => _riverMode;

        private SwitchHexTypeCommand _switchHexTypeCommand;
        private HexEditCommandRecorder _recorder;
        private HexGrid _hexGrid;
        private InputPlayer _input;
        private HexBiomeType _selectedHexBiomeType;
        private OptionalToggle _riverMode;
        private HexDirection _dragDirection;
        private Hex _previousHex;
        private Hex _currentHex;
        private int _brushSize;
        private bool _isDrug;

        public HexEditor(HexGrid hexGrid, InputPlayer input)
        {
            _input = input;
            _hexGrid = hexGrid;
            _recorder = new HexEditCommandRecorder();
            _input.ChangeClick += OnClick;
            _input.EndClick += OnEndClick;
        }

        private void EditHex(Hex hex)
        {
            ApplyHexType(hex);
            DeleteRiver(hex);
            InstallRiver(hex);
        }

        private void InstallRiver(Hex hex)
        {
            if(hex.Terrain is RoadTerrain)
                return;
            if (_isDrug && _riverMode == OptionalToggle.Yes)
            {
                var otherHex = hex.GetNeighbour(_dragDirection.Opposite());
                if (otherHex == null) return;
                if (otherHex.Terrain is RoadTerrain)
                {
                    hex.SetTerrain(new RoadTerrain(hex).SetRoad(_dragDirection));
                    return;
                }
                otherHex.SetTerrain(new RoadTerrain(otherHex).SetRoad(_dragDirection));
            }
        }

        public void EditSelectionHex(Hex hex)
        {
            if (hex == null)
            {
                _previousHex = null;
                return;
            }
            
            _currentHex = hex;
            CheckValidateDrag();
            EditHex(_currentHex);
            _previousHex = _currentHex;
        }

        private void DeleteRiver(Hex hex)
        {
            if(_riverMode == OptionalToggle.No)
                hex.DeleteTerrain();
        }

        private void ApplyHexType(Hex hex)
        {
             _switchHexTypeCommand?.UpdateNextHex(hex);   
        }

        private void OnEndClick()
        {
            if (_switchHexTypeCommand != null)
            {
                _recorder.Record(_switchHexTypeCommand);
            }
            _switchHexTypeCommand = null;
        }

        private void CheckValidateDrag()
        {
            if (_previousHex != null && _previousHex != _currentHex)
                ValidateDrag(_currentHex);
            else
                _isDrug = false;
        }

        private void ValidateDrag(Hex selectHex)
        {
            for (_dragDirection = HexDirection.NE; _dragDirection <= HexDirection.NW; _dragDirection++)
            {
                var neighbourPreviousHex = _previousHex.GetNeighbour(_dragDirection);
                if (neighbourPreviousHex == selectHex)
                {
                    _isDrug = true;
                    return;
                }
            }
            
            _isDrug = false;
        }

        private void OnClick()
        {
            _switchHexTypeCommand = new SwitchHexTypeCommand(_selectedHexBiomeType, _brushSize, _hexGrid);
        }

        private void ResetBrushSize()
        {
            if (_riverMode == OptionalToggle.Yes)
                _brushSize = 0;
        }

        public void DeleteLastAction()
        {
            _recorder.Rewind();
        }

        public void SetHexBiomeType(HexBiomeType selectType)
        {
            _selectedHexBiomeType = selectType;
        }

        public void SetRiverMode(OptionalToggle riverMode)
        {
            _riverMode = riverMode;
        }

        public void SetDragDirection(HexDirection hexDirection)
        {
            _dragDirection = hexDirection;
        }

        public void SetBrushSize(int size)
        {
            _brushSize = size;
        }

        public void Dispose()
        {
            _input.ChangeClick -= OnClick;
            _input.EndClick -= OnEndClick;
        }
    }
}