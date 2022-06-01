using Model.HexModel;
using UnityEngine;

namespace Model.SelectionsHex
{
    public class SelectionHex : IModel, ISelectionHex
    {
        public Hex SelectableHex => _selectableHex;

        private Hex _selectableHex;
        private HexGrid _hexGrid;

        public SelectionHex(HexGrid hexGrid)
        {
            _hexGrid = hexGrid;
        }
        public void SetSelectableHexCoordinate(HexCoordinates coordinate)
        {
            _selectableHex = _hexGrid.GetHex(coordinate);
        }
    }
}