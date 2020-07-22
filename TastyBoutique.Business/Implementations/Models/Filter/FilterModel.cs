using System;
using System.Collections.Generic;
using System.Text;

namespace TastyBoutique.Business.Implementations.Models.Filter
{
    public sealed class FilterModel
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
    }
}
