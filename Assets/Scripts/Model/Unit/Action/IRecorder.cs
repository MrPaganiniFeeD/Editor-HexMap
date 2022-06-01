namespace Model.Unit.Command
{
    public interface IRecorder<in T> where T : ICommand
    {
        void Record(T command);
        void Rewind();
    }
}