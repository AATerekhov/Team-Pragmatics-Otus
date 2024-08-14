using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApi.DataAccess.BusinessLogic.Models.Exceptions
{
    public class UserNotExistException(Guid userId) : Exception($"Student with id: {userId} not exist.");
  
}
