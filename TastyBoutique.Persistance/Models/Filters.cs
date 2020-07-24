using System;
using System.Collections.Generic;

namespace TastyBoutique.Persistance.Models
{
    public partial class Filters : Entity
    {
        public Filters()
        {

        }

        public Filters(string name)
        {
            Name = name;

        }
        public string Name { get; set; }
    }
}
