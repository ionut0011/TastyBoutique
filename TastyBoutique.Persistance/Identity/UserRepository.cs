using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyBoutique.Persistance.Models;

namespace TastyBoutique.Persistance.Identity
{
    public sealed class UserRepository : Repository<User>, IUserRepository
    {


        private readonly TastyBoutique_v2Context _context;

        public UserRepository(TastyBoutique_v2Context context) : base(context)
        {
            this._context = context;
        }

        public async Task<User> GetByEmail(string email) =>
            await _context.User.Where(x => x.Email == email).FirstOrDefaultAsync();
        
    }
}
