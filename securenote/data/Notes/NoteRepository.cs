using System;
using System.Threading.Tasks;
using domain;
namespace data.Notes
{
    public class NoteRepository : Abstractions.INoteRepository
    {
        public async Task<Note> GetNote(string id)
        {
            return await Task.Factory.StartNew(() => new Note());
        }
        public async Task<Guid> CreateNote(Note note)
        {
            return await Task.Factory.StartNew(Guid.NewGuid);
        }
    }
}
