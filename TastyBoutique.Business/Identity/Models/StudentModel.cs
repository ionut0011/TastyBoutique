using System;
using System.Collections.Generic;
using System.Text;

namespace TastyBoutique.Business.Identity.Models
{
     public sealed class StudentModel
    {
        public Guid IdStudent { get;set; }
        public string Name { get; set; }
        public decimal Age { get; set; }
       
    }
}
