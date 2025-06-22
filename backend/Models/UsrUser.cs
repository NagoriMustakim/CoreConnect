using LinkwayAPI.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinkwayAPI.Data
{
    public class UsrUser : IdentityUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeCode { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public int? BusinessUnitId { get; set; }

        public int? PronounId { get; set; }

        public int? DesignationId { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Country { get; set; }

        public DateTime? BirthDate { get; set; }

        public string? EmergencyContactNo { get; set; }

        public string? ProfilePhotoName { get; set; }

        public string? BannerPhotoName { get; set; }

        public string? SkypeId { get; set; }

        public string? LinkedInUrl { get; set; }

        public string? About { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }

        public virtual MstBusinessUnit? BusinessUnit { get; set; }

        public virtual ICollection<CndGiftform> CndGiftformCandidates { get; set; } = new List<CndGiftform>();

        public virtual ICollection<CndGiftform> CndGiftformGiftformReviewers { get; set; } = new List<CndGiftform>();

        public virtual MstDesignation? Designation { get; set; }

        public virtual ICollection<MstNotification> MstNotifications { get; set; } = new List<MstNotification>();

        public virtual ICollection<NmsNomination> NmsNominationNominators { get; set; } = new List<NmsNomination>();

        public virtual ICollection<NmsNomination> NmsNominationNominees { get; set; } = new List<NmsNomination>();

        public virtual MstPronoun? Pronoun { get; set; }

        public virtual ICollection<RstRequest> RstRequests { get; set; } = new List<RstRequest>();

        public virtual ICollection<UsrAward> UsrAwards { get; set; } = new List<UsrAward>();

        public virtual ICollection<UsrCertification> UsrCertifications { get; set; } = new List<UsrCertification>();

        public virtual ICollection<UsrComment> UsrCommentCommenters { get; set; } = new List<UsrComment>();

        public virtual ICollection<UsrComment> UsrCommentUsers { get; set; } = new List<UsrComment>();

        public virtual ICollection<UsrEducation> UsrEducations { get; set; } = new List<UsrEducation>();

        public virtual ICollection<UsrExperience> UsrExperiences { get; set; } = new List<UsrExperience>();

        public virtual ICollection<UsrKra> UsrKras { get; set; } = new List<UsrKra>();

        public virtual ICollection<UsrLanguage> UsrLanguages { get; set; } = new List<UsrLanguage>();

        public virtual ICollection<UsrPatent> UsrPatents { get; set; } = new List<UsrPatent>();

        public virtual ICollection<UsrProject> UsrProjects { get; set; } = new List<UsrProject>();

        public virtual ICollection<UsrSkill> UsrSkills { get; set; } = new List<UsrSkill>();

        public virtual ICollection<UsrTraining> UsrTrainings { get; set; } = new List<UsrTraining>();

        public virtual ICollection<UsrVolunteeringExperience> UsrVolunteeringExperiences { get; set; } = new List<UsrVolunteeringExperience>();

        public virtual ICollection<RstRequest> Requests { get; set; } = new List<RstRequest>();
    }
}
