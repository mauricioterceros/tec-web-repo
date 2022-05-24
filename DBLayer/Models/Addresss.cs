using System;
using System.Collections.Generic;
using System.Text;

namespace DBLayer.Models
{
    public class Addresss : Entity
    {
        public string StreetName { get; set; }

        public int Number { get; set; }

        public Guid AddressId { get; set; }

        public User User { get; set; }
    }
}
