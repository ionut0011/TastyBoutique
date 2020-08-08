using System;
using System.Collections.Generic;
using System.Text;
using TastyBoutique.Business.Models.Ingredients;

namespace TastyBoutique.UnitTesting.Shared.Extensions
{
    public static class CreateIngredientModelExtensions
    {

        public static CreateIngredientModel WithName(this CreateIngredientModel model, string name)
        {
            model.Name = name;
            return model;

        }
    }
}
