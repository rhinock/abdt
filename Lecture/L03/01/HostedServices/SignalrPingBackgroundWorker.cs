using _01.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace _01.HostedServices
{
    public class SignalrPingBackgroundWorker : BackgroundService
    {
        private readonly IHubContext<TestHub> _hubContext;

        public SignalrPingBackgroundWorker(IHubContext<TestHub> hubContext)
        {
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _hubContext.Clients.All.SendAsync(
                    "CurrentTime",
                    DateTime.Now.ToString(CultureInfo.InvariantCulture),
                    cancellationToken: stoppingToken);

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
