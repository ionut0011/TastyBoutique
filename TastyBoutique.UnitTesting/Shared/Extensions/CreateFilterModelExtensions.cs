
using TastyBoutique.Business.Models.Filter;

namespace TastyBoutique.UnitTesting.Shared.Extensions
{
   public static  class CreateFilterModelExtensions
    {

        public static CreateFilterModel WithName(this CreateFilterModel model,string name)
        {
            model.Name = name;
            return model;

        }
    }
}
