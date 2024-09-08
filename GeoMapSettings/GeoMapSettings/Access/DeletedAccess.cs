using GeoMapSettings.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMapSettings.Access
{
    public class DeletedAccess : IStrategiAccess
    {
        private SimpleHttpClient _client;
        public DeletedAccess(SimpleHttpClient client)
        {
            _client = client;
        }
        public async Task MakeTrasaction(Place place)
        {
            await _client.DeleteAsync<Place, Guid>("/api/Place", place.Id);
            Console.WriteLine($"Удален {place}");
        }
    }
}
