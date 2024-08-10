
using Back_end.Hubs;
using Back_end.Persistance;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Channels;

namespace Back_end.Services
{
    public class ApprovalService : BackgroundService, IApprovalService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Channel<Guid> _channel;
        private readonly IHubContext<RequestHub> _requestHub;

        public ApprovalService(IServiceProvider serviceProvider, IHubContext<RequestHub> requestHub)
        {
            _serviceProvider = serviceProvider;
            _channel = Channel.CreateUnbounded<Guid>();
            _requestHub = requestHub;
        }

        public async Task AddRequestToQueue(Guid requestId)
        {
            await _channel.Writer.WriteAsync(requestId);
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach (var requestId in _channel.Reader.ReadAllAsync(stoppingToken))
            {
                await ProccesRequest(requestId);
            }
        }

        private async Task ProccesRequest(Guid requestId)
        {
            await Task.Delay(TimeSpan.FromMinutes(1));

            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<Context>();

            var request = await dbContext.Requests.FindAsync(requestId);
            if (request != null)
            {
                request.Status = "įvykdytas";
                await dbContext.SaveChangesAsync();

                await _requestHub.Clients.All.SendAsync("ReceiveRequestUpdate");
            }
                
        }
    }
}
