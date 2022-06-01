using Model.SelectionsHex;
using Other.Convertible;
using UnityEngine;

namespace ViewModel.Selection
{
    public class SelectionViewModel : IViewModel
    {
        private SelectionHex _selectionHex;
        
        public SelectionViewModel(SelectionHex selectionHex)
        {
            _selectionHex = selectionHex;
        }

        public void SetSelectableHexPosition(Vector3 hexPosition) => 
            _selectionHex.SetSelectableHexCoordinate(hexPosition.GetConvertibleCoordinates());
    }
}