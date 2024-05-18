using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Services.Repositories.Abstractions;

namespace Infrastructure.Repositories.Implementations
{
    public class PlaceTypeRepository:Repository<PlaceType,int>, IPlaceTypeRepository
    {
        public PlaceTypeRepository(DatabaseContext context):base(context)
        {                
        }
    }
}
