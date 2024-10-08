﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repositories.Abstractions
{
    public interface IPlaceRepository:IRepository<Place,Guid>
    {
       Task<List<Place>> GetForTypeAsync(int placeTypeId, CancellationToken cancellationToken);
        Task<List<Place>> TrasingByTypeAsync(int placeTypeId, Road road, CancellationToken cancellationToken);
    }
}
