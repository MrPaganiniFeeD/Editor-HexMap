using Model.HexModel;

namespace DefaultNamespace.TemplateCommand
{
    public interface IHexTemplate : ITemplate
    {
        IHexInfo Info { get; }
    }
}