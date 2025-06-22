namespace LinkwayAPI.DTOs.Department
{
    public class DepartmentViewDTO
    {
        public Guid DepartmentGuid { get; set; }

        public string Department { get; set; } = null!;

        public string? DepartmentDescription { get; set; }
    }
}
