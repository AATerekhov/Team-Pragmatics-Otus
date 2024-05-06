using Domain.Entities;
using Infrastructure.EntityFramework;
using Services.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implementations
{
    public class UserRepository:Repository<User, int>, IUserRepository
    {
        public UserRepository(DatabaseContext context): base(context)
        {               
        }
    }
}
