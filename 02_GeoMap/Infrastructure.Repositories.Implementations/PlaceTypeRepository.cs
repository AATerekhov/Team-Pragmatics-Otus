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

        public override async Task<PlaceType> AddAsync(PlaceType entity)
        {
            await base.AddAsync(entity);
            await base.SaveChangesAsync();
            return entity;
        }
    }
}
