using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace TastyBoutique.Business.Implementations.Models.Recipe
{
    public sealed class GetPhotoModel
    {
        public IFormFile Image { get; set; }
    }
}
