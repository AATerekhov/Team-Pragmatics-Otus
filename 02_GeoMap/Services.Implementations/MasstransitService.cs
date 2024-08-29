using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class MasstransitService(ILogger<MasstransitService> logger, IBusControl busControl) : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await busControl.StartAsync(cancellationToken); 
        }

        public  async Task StopAsync(CancellationToken cancellationToken)
        {
            await busControl.StopAsync(cancellationToken);
        }
    }
}
