namespace Model.Unit.Command
{
    public interface IUnitAction : ICommand
    {
        Unit Unit { get; }
        
    }
}