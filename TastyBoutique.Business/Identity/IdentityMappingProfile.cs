using AutoMapper;
using TastyBoutique.Business.Identity.Models;
using TastyBoutique.Persistance.Models;

namespace TastyBoutique.Business.Identity
{
     public class IdentityMappingProfile : Profile
    {
        public IdentityMappingProfile()
        {
            CreateMap<User, UserModel>();
        }

    }
}
