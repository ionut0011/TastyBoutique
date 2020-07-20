using System;
using System.Collections.Generic;
using System.Text;

namespace TastyBoutique.Persistance
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
