using System;
using System.Collections.Generic;
using System.Text;

namespace TastyBoutique.Entities
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
    }
}
