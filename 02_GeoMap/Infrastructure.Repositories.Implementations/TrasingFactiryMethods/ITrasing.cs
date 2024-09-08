using Domain.Entities;
using GeometryMap.GeometricModel.Figure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implementations.TrasingFactiryMethods
{
    public interface ITrasing
    {
        int PlaceTipeId { get; }
        bool OnTheRoad(Place place);
        ABCD GetBoard();
    }
}
