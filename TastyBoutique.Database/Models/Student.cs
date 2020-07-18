using System;
using System.Collections.Generic;

namespace TastyBoutique.Database.Models
{
    public partial class Student
    {
        public Student()
        {
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Age { get; set; }
        public string Email { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
