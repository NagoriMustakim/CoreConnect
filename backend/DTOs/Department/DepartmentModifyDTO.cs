namespace LinkwayAPI.DTOs.Department
{
    public class DepartmentModifyDTO
    {
        public Guid DepartmentGuid { get; set; }

        public string Department { get; set; } = null!;

        public string? DepartmentDescription { get; set; }
    }
}
