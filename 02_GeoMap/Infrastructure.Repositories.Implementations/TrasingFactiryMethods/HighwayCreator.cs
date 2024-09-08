using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implementations.TrasingFactiryMethods
{
    public class HighwayCreator : TrasingCreator
    {
        public HighwayCreator():base()
        {
        }
        public override ITrasing Trasing(Road road,int typePlace) 
        {
            return new TrasingHighway(road,typePlace);
        }
    }
}
