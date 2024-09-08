using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implementations.TrasingFactiryMethods
{
    public abstract class TrasingCreator
    {
        public abstract ITrasing Trasing(Road road, int typePlace);
        public async Task<List<Place>> GetPlace(Road road, int typePlace, DbContext dbContext) 
        {
            var trasing = Trasing(road, typePlace);

            var borders = trasing.GetBoard();
            var resultDB = (await dbContext.Set<Place>()
               .Where(l => !l.Deleted && l.PlaceTypeID == trasing.PlaceTipeId
               && l.Longitude >= borders.MinX
               && l.Longitude <= borders.MaxX
               && l.Latitude >= borders.MinY
               && l.Latitude <= borders.MaxY)
               .ToListAsync());

            return await Task.FromResult(resultDB.AsParallel().Where(l => trasing.OnTheRoad(l)).ToList());
        } 
    }
}
