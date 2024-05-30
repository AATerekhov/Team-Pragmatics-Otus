using Domain.Entities;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Services.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implementations
{
    public class FuellingRepository:Repository<Fuelling,Guid>,IFuellingRepository
    {
        public FuellingRepository(DatabaseContext databaseContext):base(databaseContext) { }

        public override async Task<Fuelling> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var query = Context.Set<Fuelling>().AsQueryable();
            query = query.Where(l => l.Id == id && !l.Deleted);
            return await query.SingleOrDefaultAsync();
        }
    }
}
