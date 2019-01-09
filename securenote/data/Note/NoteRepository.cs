using System;
using System.Threading.Tasks;

namespace data.Notes
{
    public class NoteRepository : Abstractions.INoteRepository
    {
        public async Task<domain.Notes.Note> GetNote(string id)
        {
            return await Task.Factory.StartNew(() => new domain.Notes.Note());
        }
        public async Task<Guid> CreateNote(domain.Notes.Note note)
        {
            return await Task.Factory.StartNew(Guid.NewGuid);
        }
    }
}
