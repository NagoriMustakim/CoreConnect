namespace LinkwayAPI.DTOs.Comment
{
    public class CommentListDTO
    {
        public Guid CommentGuid { get; set; }

        public Guid CommenterGuid { get; set; }

        public string? CommenterProfilePhotoName { get; set; }

        public string CommenterName { get; set; } = null!;

        public string Comment { get; set; } = null!;

        public DateTime ModificationDate { get; set; }
    }
}
