namespace LinkwayAPI.DTOs.Request
{
    public class RequestListDTO
    {
        public Guid RequestGuid { get; set; }

        public int RequestType { get; set; }

        public string RequestDescription { get; set; } = null!;

        public int RequestStatus { get; set; }

        public int NoOfMembers { get; set; }

        public DateTime RequestDate { get; set; }

        public string? RequestRejectionReason { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }

        public virtual ICollection<UserListDTO> Users { get; set; }

    }
}
