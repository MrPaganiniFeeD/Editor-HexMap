using System.Collections.Generic;
using Zenject;

namespace Model.Unit.Command
{
    public class ActionUnitRecorder : IRecorder<IUnitAction>
    {
        private readonly Stack<IUnitAction> _actions = new Stack<IUnitAction>(); 

        public void Record(IUnitAction action)
        {
            _actions.Push(action);
            action.Execute();
        }
        
        public void Rewind()
        {
            var action = _actions.Pop();
            action.Undo();
        }
    }
}