using System;
using Commander.Common;

namespace Commander.Test.Data
{
    public class Model : AggregateRoot
    {
        public string For { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
    }
}