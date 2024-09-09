using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApi.DataAccess.Entities;
using UserApi.DataAccess.EntityFramework;
using UserApi.DataAccess.Repositories.Abstractions;

namespace UserApi.DataAccess.Repositories.Implementations
{
    public class UserRepository : Repository<User, Guid>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
        }
    }
}
