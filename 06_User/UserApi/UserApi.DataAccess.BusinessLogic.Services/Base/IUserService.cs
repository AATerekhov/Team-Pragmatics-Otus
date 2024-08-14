using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApi.DataAccess.BusinessLogic.Models;

namespace UserApi.DataAccess.BusinessLogic.Services.Base
{
    public interface IUserService
    {
        Task<UserModel?> GetUserAsync(Guid id);
        Task<IEnumerable<UserModel>> GetUsersAsync();
        Task<UserModel> CreateUserAsync(CreateUserModel user);
        Task UpdateUserAsync(UserModel user);
        Task DeleteUserAsync(Guid id);
    }
}
