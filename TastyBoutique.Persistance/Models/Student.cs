using System;
using System.Collections.Generic;

namespace TastyBoutique.Persistance.Models
{
    public partial class Student : Entity
    {
        public Student()
        {
            User = new HashSet<User>();
        }

        public string Name { get; set; }
        public decimal Age { get; set; }
        public string Email { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
