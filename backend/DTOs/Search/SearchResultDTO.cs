namespace LinkwayAPI.DTOs.Search
{
    public class SearchResultDTO
    {
        public string UserId { get; set; }
        public string EmployeeName { get; set; }
        public string ProfilePhotoName { get; set; }
        public string Designation { get; set; }
        public IList<string> Role { get; set; }

    }
}
