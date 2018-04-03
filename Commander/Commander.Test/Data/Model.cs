using System;
using Commander.Domain;

namespace Commander.Test.Data
{
    public class Model : IAggregateRoot
    {
        public string For { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}