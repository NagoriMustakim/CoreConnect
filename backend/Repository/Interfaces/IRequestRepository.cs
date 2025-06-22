using LinkwayAPI.DTOs.Request;
using System.Security.Claims;

namespace LinkwayAPI.Repository.Interfaces
{
    public interface IRequestRepository
    {
        Task<RequestListDTO> CreateRequestAsync(RequestCreateDTO request, Claim? email);
        Task<(IEnumerable<RequestListAdminDTO> List, int TotalCount)> GetAllRequestsAsync(List<int> status, int pageNumber = 1, int pageSize = 10);
        Task<(IEnumerable<RequestListDTO> List, int TotalCount)> GetAllRequestsByUserIdAsync(int employeeCode, List<int> status, int pageNumber, int pageSize);
        Task<RequestListDTO> GetRequestByIdAsync(Guid requestId);
        Task<bool> DeleteRequestAsync(Guid requestId);
        Task<bool> ApproveOrRejectRequestAsync(Guid requestId, RequestStatusUpdateDTO requestUpdate);
        Task<bool> ModifyRequestAsync(RequestModifyDTO request);
    }
}
