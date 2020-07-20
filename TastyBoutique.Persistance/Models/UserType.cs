using System;
using System.Collections.Generic;

namespace TastyBoutique.Persistance.Models
{
    public partial class UserType : Entity
    {
        public UserType()
        {
            User = new HashSet<User>();
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
