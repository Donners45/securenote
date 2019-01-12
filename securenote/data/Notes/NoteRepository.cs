using System;
using System.Threading.Tasks;
using data.Adapters.Redis.Abstractions;
using domain;
using Microsoft.Extensions.Logging;

namespace data.Notes
{
    public class NoteRepository : Abstractions.INoteRepository
    {
        private readonly IRedisConnectionFactory _factory;

        public NoteRepository(IRedisConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<Note> GetNote(string id)
        {
            var dbContext = _factory.GetConnection().GetDatabase();
            var exists = await dbContext.KeyExistsAsync(id);
            if (exists)
            {
                var tran = dbContext.CreateTransaction();
                var getResult = tran.StringGetAsync(id);

                tran.KeyDeleteAsync(id);
                tran.Execute();

                var message = (string)getResult.Result;

                return new Note(new Guid(id)) { Message = message };
            }
            return null;
        }

        public async Task<Guid> CreateNote(Note note)
        {

            var dbContext = _factory.GetConnection().GetDatabase();
            var OK = await dbContext.StringSetAsync(note.Id.ToString(), note.Message);
            return OK ? note.Id : Guid.Empty;
        }
    }
}
