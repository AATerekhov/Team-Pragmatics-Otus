using Domain.Entities;
using Infrastructure.EntityFramework;
using Infrastructure.Repositories.Implementations.Extentions;
using Infrastructure.Repositories.Implementations.TrasingFactiryMethods;
using Microsoft.EntityFrameworkCore;
using Services.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implementations
{
    public class PlaceRepository:Repository<Place,Guid>,IPlaceRepository
    {
        private TrasingCreator _trasingCreator;
        public PlaceRepository(DatabaseContext databaseContext, TrasingCreator trasingCreator) :base(databaseContext) 
        {
            _trasingCreator = trasingCreator;
        }

        public override async Task<Place> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var query = Context.Set<Place>().AsQueryable();
            query = query.Where(l => l.Id == id && !l.Deleted);

            return await query.SingleOrDefaultAsync();
        }

        public async Task<List<Place>> GetForTypeAsync(int placeTypeId, CancellationToken cancellationToken)
        {
            var query = GetAll().Where(l => !l.Deleted && l.PlaceTypeID == placeTypeId);
            return await query.ToListAsync();
        }

        public async Task<List<Place>> TrasingByTypeAsync(int placeTypeId, Road road, CancellationToken cancellationToken)
        {
            return await _trasingCreator.GetPlace(road,placeTypeId,Context);
        }
    }
}
