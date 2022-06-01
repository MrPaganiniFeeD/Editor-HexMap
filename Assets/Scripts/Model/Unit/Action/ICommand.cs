namespace Model.Unit.Command
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}