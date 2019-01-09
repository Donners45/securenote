using System;
namespace domain.Notes
{
    public class Note
    {
        public Note()
        {
            Id = Guid.NewGuid();
        }

        public Note(Guid id)
        {
            this.Id = id;
        }

        public string Message { get; set; }
        public Guid Id { get; }
    }
}
