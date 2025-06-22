using LinkwayAPI.DTOs.BusinessUnit;
using LinkwayAPI.DTOs.User.Certificate;
using LinkwayAPI.DTOs.User.Experience;
using LinkwayAPI.DTOs.User.Project;
using LinkwayAPI.DTOs.User.Skill;
using LinkwayAPI.DTOs.User.Training;
using LinkwayAPI.DTOs.User.UsrLanguage;

namespace LinkwayAPI.DTOs.Resume
{
    public class ResumeExportDTO
     {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Designation { get; set; }
        public string About { get; set; }
        public BusinessUnitListDTO BusinessUnit { get; set; }
        public List<UsrExperienceViewDTO> Experiences{ get; set; }
        public List<UsrSkillDisplayDTO> Skills { get; set; }
        public List<UsrLanguageDisplayDTO> Language { get; set; }
        public List<UsrProjectDisplayDTO> Projects { get; set; }
        public List<UsrCertificateViewDTO> Certifications { get; set; }
        public List<UsrTrainingDisplayDTO> Trainings { get; set; }

    }
}
