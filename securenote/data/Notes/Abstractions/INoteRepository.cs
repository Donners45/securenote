using System;
using System.Threading.Tasks;
using domain;

namespace data.Notes.Abstractions
{
    public interface INoteRepository
    {
        Task<Guid> CreateNote(Note note);
        Task<Note> GetNote(string id);
    }
}