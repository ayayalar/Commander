using System;
using Commander.Common;

namespace Commander.Test.Data
{
    public class Model : AggregateRoot
    {
        public Model(long id, string name)
        {
            Id = id;
            Name = name;
            CreatedAt = DateTime.Today;
        }
        public string Name { get; }
        public DateTime CreatedAt { get; }
        public bool IsUpdated { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public void Update()
        {
            IsUpdated = true;
            UpdatedAt = DateTime.Today;
        }
    }
}