using System;
using domain;

namespace data.Notes
{
    public class NoteLogic : INoteLogic
    {
        public Note Create(string message)
        {
            if (string.IsNullOrEmpty(message)) return null;

            return new Note { Message = message };
        }
    }
}
