using LinkwayAPI.Enums.Request;
using System.ComponentModel.DataAnnotations;

namespace LinkwayAPI.DTOs.Request
{
    public class RequestStatusUpdateDTO
    {
        [Range(0,2)]
        public RequestStatus Status { get; set; }
        [MaxLength(500)]
        public string? RejectionReason { get; set; }
    }
}
