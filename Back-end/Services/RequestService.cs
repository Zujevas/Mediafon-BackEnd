using Back_end.Hubs;
using Back_end.Models;
using Back_end.Persistance;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Back_end.Services
{
    public class RequestService : IRequestService
    {
        private readonly Context _context;
        private readonly IApprovalService _approvalService;
        private readonly IHubContext<RequestHub> _requestHub;
        public RequestService(Context context, IApprovalService approvalService, IHubContext<RequestHub> requestHub)
        {
            _context = context;
            _approvalService = approvalService;
            _requestHub = requestHub;
        }

        public async Task<List<Request>> getAllRequests()
        {
            var result = await _context.Requests.ToListAsync();

            return result;
        }

        public async Task<Request> createRequest(CreateRequestDto request)
        {
            var newRequest = new Request
            {
                Type = request.Type,
                Message = request.Message,
                Status = "pateiktas",
                Date = DateTime.UtcNow
            };

            _context.Requests.Add(newRequest);
            await _context.SaveChangesAsync();

            await _approvalService.AddRequestToQueue(newRequest.Id);

            await _requestHub.Clients.All.SendAsync("ReceiveRequestUpdate");

            return newRequest;
        }
    }
}
