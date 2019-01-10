using System;
using domain;

namespace data.Notes
{
    public class NoteLogic
    {
        public Note Create(string message)
        {
            if (string.IsNullOrEmpty(message)) return null;

            return new Note { Message = message };
        }
    }
}
