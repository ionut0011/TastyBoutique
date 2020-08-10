using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using AutoMapper;
using AutoMapper.Configuration.Annotations;

namespace TastyBoutique.Business.Models.Recipe
{
    public sealed class UpsertRecipeModel
    {
        public string Name { get; set; }
        public Boolean Access { get; set; }
        public string Description { get; set; }

        public byte[] Image { get; set; }
        public string Type { get; set;  }

        [JsonIgnore]
        public Guid IdUser { get; set; }

        public IList<string> Ingredients { get; set; }

        public string Filter { get; set; }
    }
}
