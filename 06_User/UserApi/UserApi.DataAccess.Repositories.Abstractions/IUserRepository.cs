using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApi.DataAccess.Entities;

namespace UserApi.DataAccess.Repositories.Abstractions
{
    public interface IUserRepository : IRepository<User, Guid>
    {

    }
}
