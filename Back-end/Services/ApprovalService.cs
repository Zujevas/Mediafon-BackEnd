
using Back_end.Persistance;
using System.Threading.Channels;

namespace Back_end.Services
{
    public class ApprovalService : BackgroundService, IApprovalService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Channel<Guid> _channel;

        public ApprovalService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _channel = Channel.CreateUnbounded<Guid>();
        }

        public async Task AddRequestToQueue(Guid requestId)
        {
            await _channel.Writer.WriteAsync(requestId);
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach (var requestId in _channel.Reader.ReadAllAsync(stoppingToken))
            {
                await PriccesRequest(requestId);
            }
        }

        private async Task PriccesRequest(Guid requestId)
        {
            await Task.Delay(TimeSpan.FromMinutes(1));

            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<Context>();

            var request = await dbContext.Requests.FindAsync(requestId);
            if (request != null)
            {
                request.Status = "įvykdytas";
                await dbContext.SaveChangesAsync();
            }
                
        }
    }
}
