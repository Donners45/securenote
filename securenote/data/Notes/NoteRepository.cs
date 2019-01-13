using System;
using System.Threading.Tasks;
using data.Adapters.Redis.Abstractions;
using domain;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

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
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                tran.KeyDeleteAsync(id);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
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

        /// <summary>
        /// Returns value from key, scrubbing the value in store.
        /// </summary>
        /// <returns>The and scrub.</returns>
        /// <param name="dbContext">Db context.</param>
        /// <param name="key">Key.</param>
        private async Task<string> GetAndScrub(IDatabase dbContext, string key)
        {
            if (await dbContext.KeyExistsAsync(key))
            {
                var result = await dbContext.StringGetSetAsync(key, string.Empty);
                return result;
            }
            return null;
        }
    }
}
