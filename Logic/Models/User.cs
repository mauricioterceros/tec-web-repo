using System;

namespace Logic.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string CI { get; set; }
        public string Name { get; set; }

        public string LastName { get; set; }
    }
}