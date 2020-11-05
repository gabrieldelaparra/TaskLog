using System;

namespace TaskLog.Core.Models
{
    public class Task
    {
        public Task() {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; }
        public Guid ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public IList<Guid> WorkIds { get; set; } = new List<Guid>();
    }
}
