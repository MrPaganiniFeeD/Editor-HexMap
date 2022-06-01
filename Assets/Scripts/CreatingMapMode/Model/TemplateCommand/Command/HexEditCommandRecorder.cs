using System.Collections.Generic;
using Model.Unit.Command;

namespace DefaultNamespace.TemplateCommand.Command
{
    public class HexEditCommandRecorder : IRecorder<ITemplateCommand>
    {
        private readonly Stack<ITemplateCommand> _actions = new Stack<ITemplateCommand>();

        public void Record(ITemplateCommand command)
        {
            _actions.Push(command);
            command.Execute();
        }

        public void Rewind()
        {
            if(_actions.Count == 0)
                return;
            var action = _actions.Pop();
            action.Undo();
        }
    }
} 