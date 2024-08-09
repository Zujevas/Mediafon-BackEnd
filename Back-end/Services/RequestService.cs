using Back_end.Models;
using Back_end.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Back_end.Services
{
    public class RequestService : IRequestService
    {
        private readonly Context _context;
        public RequestService(Context context)
        {
            _context = context;
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
                Status = "pending",
                Date = DateTime.UtcNow
            };

            _context.Requests.Add(newRequest);
            await _context.SaveChangesAsync();

            return newRequest;
        }
    }
}
