using domain;

namespace data.Notes.Abstractions
{
    public interface INoteLogic
    {
        Note Create(string message);
    }
}