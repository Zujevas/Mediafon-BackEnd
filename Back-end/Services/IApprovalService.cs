namespace Back_end.Services
{
    public interface IApprovalService
    {
        Task AddRequestToQueue(Guid requestId);
    }
}
