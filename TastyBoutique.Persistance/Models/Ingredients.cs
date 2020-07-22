using System;
using System.Collections.Generic;

namespace TastyBoutique.Persistance.Models
{
    public partial class Ingredients : Entity
    {
        public Ingredients(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
