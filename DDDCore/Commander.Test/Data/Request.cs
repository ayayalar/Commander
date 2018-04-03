using System;

namespace Commander.Test.Data
{
    public class Request
    {
        public string For { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Today;
    }
}