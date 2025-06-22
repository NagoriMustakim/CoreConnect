namespace LinkwayAPI.DTOs.Designation
{
    public class DesignationEditDTO
    {
        public Guid DesignationGuid { get; set; }
        public Guid DepartmentGuid { get; set; }
        public string? Designation { get; set; }
        public string? DesignationDescription { get; set; }
        public int DesignationLevel { get; set; }
    }
}
