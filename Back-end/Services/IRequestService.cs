using Back_end.Models;

namespace Back_end.Services
{
    public interface IRequestService
    {
        Task<List<Request>> getAllRequests();
        Task<Request> createRequest(CreateRequestDto request);
    }
}
