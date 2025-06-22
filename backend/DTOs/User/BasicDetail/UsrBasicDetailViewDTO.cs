namespace LinkwayAPI.DTOs.User.BasicDetail
{
    public class UsrBasicDetailViewDTO
    {
        public string Id { get; set; }

        public int EmployeeCode { get; set; }

        public Guid? PronounGuid { get; set; }

        public string? Pronoun { get; set; }

        public Guid? BusinessUnitGuid { get; set; }

        public string? BusinessUnitName { get; set; } = null!;

        public string BusinessUnitLogoName { get; set; } = null!;

        public string? PhoneNumber { get; set; } = null!;

        public string? EmergencyContactNo { get; set; }

        public string? Email { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public Guid? DesignationGuid { get; set; }

        public string? Designation { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Country { get; set; }

        public DateTime? BirthDate { get; set; }

        public string? About { get; set; }

        public string? ProfilePhotoName { get; set; }

        public string? BannerPhotoName { get; set; }

        public IList<string> Role { get; set; }

        public DateTime JoiningDate { get; set; }

        public string? SkypeId { get; set; }

        public string? LinkedInUrl { get; set; }

    }
}
