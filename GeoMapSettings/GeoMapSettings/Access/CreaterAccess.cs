using GeoMapSettings.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMapSettings.Access
{
    public class CreaterAccess : IStrategiAccess
    {
        private SimpleHttpClient _client;

        public CreaterAccess(SimpleHttpClient client)
        {
            _client = client;
        }

        public async Task MakeTrasaction(Place place)
        {
            await _client.PostAsyncNotResult("/api/Place", place);
            Console.WriteLine($"Загружен {place}");
        }
    }
}
