namespace LinkwayAPI.DTOs.Designation
{
    public class DesignationViewDTO
    {
        public Guid DepartmentGuid { get; set; }
        public string Department { get; set; } = null!;
        public Guid DesignationGuid { get; set; }
        public string Designation { get; set; }
        public string? DesignationDescription { get; set; }
        public int DesignationLevel { get; set; }
    }
}
