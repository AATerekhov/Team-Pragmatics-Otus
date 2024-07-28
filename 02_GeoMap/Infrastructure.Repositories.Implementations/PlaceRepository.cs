using Domain.Entities;
using Infrastructure.EntityFramework;
using Infrastructure.Repositories.Implementations.Extentions;
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
        public PlaceRepository(DatabaseContext databaseContext):base(databaseContext) { }

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
            var borders = road.Borders();
            var resultDB = (await Context.Set<Place>()
                .Where(l => !l.Deleted && l.PlaceTypeID == placeTypeId 
                && l.Longitude >= borders.MinX
                && l.Longitude <= borders.MaxX 
                && l.Latitude >= borders.MinY
                && l.Latitude <= borders.MaxY)
                .ToListAsync());
            var result = resultDB.AsParallel().Where(l => road.OnTheRoad(l)).ToList();
            return await Task.FromResult(result);
        }
    }
}
