using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TastyBoutique.Persistance.Models;

namespace TastyBoutique.Persistance.Identity
{
    public sealed class UserRepository : Repository<User>, IUserRepository
    {


        private readonly TastyBoutiqueContext _context;

        public UserRepository(TastyBoutiqueContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.User.Where(x => x.Email.Equals(email)).FirstOrDefaultAsync();
        }
    }
}
