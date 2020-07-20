using System;
using System.Collections.Generic;

namespace TastyBoutique.Persistance.Models
{
    public partial class Ingredients : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
