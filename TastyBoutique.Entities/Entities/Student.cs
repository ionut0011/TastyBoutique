using System;
using System.Collections.Generic;

namespace TastyBoutique.Persistance.Models
{
    public class Student : Entity
    {
        public Student()
        {

        }
        public Student(string Name,decimal Age,string Email)
        {
            this.Name = Name;
            this.Age = Age;
            this.Email = Email;
            User = new HashSet<User>();
        }
        public string Name { get; set; }
        public decimal Age { get; set; }
        public string Email { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
