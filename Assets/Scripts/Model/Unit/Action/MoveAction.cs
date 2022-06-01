using Model.HexModel;
using UnityEngine;

namespace Model.Unit.Command
{
    public class MoveAction : IUnitAction
    {
        public Unit Unit { get; }
        private Hex _targetHex;
        private Hex _startHex;

        public MoveAction(Unit unit, Hex targetHex, Hex startHex)
        {
            Unit = unit;
            _targetHex = targetHex;
            _startHex = startHex;
        }
        public void Execute()
        {
            Unit.Move(_targetHex);
        }

        public void Undo()
        {
            Unit.Move(_startHex);
        }
    }
}