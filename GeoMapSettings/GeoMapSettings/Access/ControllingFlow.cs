using GeoMapSettings.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMapSettings.Access
{
    public class ControllingFlow
    {
        private Semaphore? _semaphoreDb;
        private IStrategiAccess? _strategiAccess;

        public void SetSemathore(int amountOfAccess) 
        {
            _semaphoreDb = new Semaphore(amountOfAccess, amountOfAccess, "dbAccess");            
        }
        public void SetAccess(IStrategiAccess strategiAccess)
        {
            _strategiAccess = strategiAccess;
        }
        public async Task MakeStrategi(Place place) 
        {
            try
            {
                _semaphoreDb!.WaitOne();
                await _strategiAccess!.MakeTrasaction(place);
            }
            finally
            {
                _semaphoreDb!.Release();                
            }
        }
    }
}
