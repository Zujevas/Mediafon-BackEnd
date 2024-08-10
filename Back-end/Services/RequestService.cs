using Back_end.Models;
using Back_end.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Back_end.Services
{
    public class RequestService : IRequestService
    {
        private readonly Context _context;
        private readonly IApprovalService _approvalService;
        public RequestService(Context context, IApprovalService approvalService)
        {
            _context = context;
            _approvalService = approvalService;
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

            return newRequest;
        }
    }
}
