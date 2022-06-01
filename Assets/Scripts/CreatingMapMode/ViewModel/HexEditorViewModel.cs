using System;
using CreatingMapMode.Model;
using CreatingMapMode.View;
using Model.HexModel;
using Other.Convertible;
using UniRx;
using UnityEngine;
using ViewModel;

namespace CreatingMapMode.ViewModel
{
    public class HexEditorViewModel : IViewModel, IDisposable
    {
        private ReactiveProperty<OptionalToggle> _riverMode = new ReactiveProperty<OptionalToggle>();
        private ReactiveProperty<HexBiomeType> _hexBiomeType = new ReactiveProperty<HexBiomeType>();
        private ReactiveProperty<int> _brushSize = new ReactiveProperty<int>();

        private HexEditor _hexEditor;
        private HexGrid _hexGird;

        public HexEditorViewModel(HexEditor hexEditor, HexGrid hexGrid)
        {
            _hexGird = hexGrid;
            _hexEditor = hexEditor;
            _hexEditor.UpdateValues += OnUpdateValues;
        }

        private void OnUpdateValues()
        {
            _hexBiomeType.Value = _hexEditor.SelectedHexBiomeType;
            _riverMode.Value = _hexEditor.RiverMode;
            _brushSize.Value = _hexEditor.BrushSize;
        }

        public void EditSelectionHex(Vector3 position)
        {
            if(position == Vector3.zero)
            {
                _hexEditor.EditSelectionHex(null);
                return;
            }
            
            Hex hex = _hexGird.GetHex(position.GetConvertibleCoordinates());
            _hexEditor.EditSelectionHex(hex);
        }

        public void SetBrushSize(int size) => 
            _hexEditor.SetBrushSize(size);

        public void SetBiomeType(HexBiomeType biomeType) => 
            _hexEditor.SetHexBiomeType(biomeType);

        public void SetRiverMode(OptionalToggle riverMode) => 
            _hexEditor.SetRiverMode(riverMode);

        public void DeleteLastAction() => 
            _hexEditor.DeleteLastAction();

        public ReactiveProperty<OptionalToggle> GetRiverMode() => 
            _riverMode;

        public ReactiveProperty<HexBiomeType> GetBiomeType() => 
            _hexBiomeType;

        public ReactiveProperty<int> GetBrushSize() => 
            _brushSize;

        public void Dispose() => 
            _hexEditor.UpdateValues -= OnUpdateValues;
    }
}