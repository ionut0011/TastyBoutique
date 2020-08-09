using System;
using System.Collections.Generic;

namespace TastyBoutique.Persistance.Models
{
    public class Filters : Entity
    {
        public Filters()
        {

        }

        public Filters(string name)
        {
            Name = name;
            RecipesFilters = new HashSet<RecipesFilters>();

        }
        public string Name { get; set; }

        public ICollection<RecipesFilters> RecipesFilters { get; set; }
    }
}
