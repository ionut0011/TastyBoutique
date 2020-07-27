using System;
using System.Collections.Generic;
using System.Text;

namespace TastyBoutique.Business.Identity.Services.Interfaces
{
    public interface IPasswordHasher
    {

        string CreateHash(string password);

        bool Check(string hash, string password);

    }
}
