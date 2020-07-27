using System.Threading.Tasks;
using TastyBoutique.Persistance.Models;

namespace TastyBoutique.Persistance.Identity
{
   public  interface IUserRepository : IRepository<User>
    {

        Task<User> GetByEmail(string email);
       
    }
}
