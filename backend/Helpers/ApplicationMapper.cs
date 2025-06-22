using AutoMapper;
using LinkwayAPI.Data;
using LinkwayAPI.DTOs.Attachment;
using LinkwayAPI.DTOs.BusinessUnit;
using LinkwayAPI.DTOs.Certification;
using LinkwayAPI.DTOs.Comment;
using LinkwayAPI.DTOs.Componies;
using LinkwayAPI.DTOs.Department;
using LinkwayAPI.DTOs.Designation;
using LinkwayAPI.DTOs.EmploymentType;
using LinkwayAPI.DTOs.GiftProgram;
using LinkwayAPI.DTOs.InternalPrograms;
using LinkwayAPI.DTOs.InternalProgramsCategories;
using LinkwayAPI.DTOs.LocationType;
using LinkwayAPI.DTOs.Nomination;
using LinkwayAPI.DTOs.Notification;
using LinkwayAPI.DTOs.Proficiency;
using LinkwayAPI.DTOs.Project;
using LinkwayAPI.DTOs.Pronoun;
using LinkwayAPI.DTOs.Request;
using LinkwayAPI.DTOs.Skill;
using LinkwayAPI.DTOs.Training;
using LinkwayAPI.DTOs.TrainingType;
using LinkwayAPI.DTOs.User;
using LinkwayAPI.DTOs.User.BasicDetail;
using LinkwayAPI.DTOs.User.Certificate;
using LinkwayAPI.DTOs.User.Education;
using LinkwayAPI.DTOs.User.Experience;
using LinkwayAPI.DTOs.User.Kra;
using LinkwayAPI.DTOs.User.Project;
using LinkwayAPI.DTOs.User.Skill;
using LinkwayAPI.DTOs.User.Training;
using LinkwayAPI.DTOs.User.UsrLanguage;
using LinkwayAPI.Models;

namespace LinkwayAPI.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<MstSkill, SkillListDTO>();
            CreateMap<SkillCreateUpdateDTO, MstSkill>()
                .ForMember(dest => dest.SkillGuid, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<MstTraining, TrainingListDTO>();
            CreateMap<TrainingCreateDTO, MstTraining>()
                .ForMember(dest => dest.TrainingGuid, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<TrainingModifyDTO, MstTraining>()
                .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<MstCertification, CertificationViewDTO>();
            CreateMap<CertificationAddDTO, MstCertification>()
                .ForMember(dest => dest.CertificationGuid, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<CertificationEditDTO, MstCertification>()
                .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<RequestCreateDTO, RstRequest>()
            .ForMember(dest => dest.Users, opt => opt.Ignore())
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.RequestType, opt => opt.MapFrom(src => (int)src.Type));

            CreateMap<RequestStatusUpdateDTO, RstRequest>()
            .ForMember(dest => dest.RequestStatus, opt => opt.MapFrom(src => (int)src.Status))
            .ForMember(dest => dest.RequestRejectionReason, opt => opt.MapFrom(src => src.RejectionReason))
            .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<RequestModifyDTO, RstRequest>()
            .ForMember(dest => dest.RequestType, opt => opt.MapFrom(src => (int)src.Type))
            .ForMember(dest => dest.RequestDescription, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.NoOfMembers, opt => opt.MapFrom(src => src.NoOfMembers))
            .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UsrUser, UserListDTO>();

            CreateMap<RstRequest, RequestListDTO>();
            CreateMap<UsrAward, UsrAwardDTO>().ReverseMap();

            CreateMap<MstEmploymentType, EmploymentTypeDTO>().ReverseMap();
            CreateMap<EmploymentTypeDTO, MstEmploymentType>()
                .ForMember(dest => dest.EmploymentTypeGuid, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<EmploymentTypeModifyDTO, MstEmploymentType>()
            .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<MstEmploymentType, EmploymentTypeViewDTO>();

            CreateMap<LocationTypeDTO, MstLocationType>()
                .ForMember(dest => dest.LocationTypeGuid, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<LocationTypeModifyDTO, MstLocationType>()
                .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<MstLocationType, LocationTypeViewDTO>();

            CreateMap<TrainingTypeDTO, MstTrainingType>()
                .ForMember(dest => dest.TrainingTypeGuid, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<MstTrainingType, TrainingTypeViewDTO>().ReverseMap();

            CreateMap<TrainingTypeModifyDTO, MstTrainingType>()
                .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<RstRequest, RequestListDTO>().ReverseMap();
            CreateMap<RstRequest, RequestListAdminDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Requester.FirstName + " " + src.Requester.LastName));

            CreateMap<GiftProgramDTO, CndGiftform>()
            .ForMember(dest => dest.GiftformGuid, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<CndGiftform, GiftProgramDisplayDTO>()
                .ForMember(dest => dest.GiftformReviewerName, opt => opt.MapFrom(src => src.GiftformReviewer.FirstName + " " + src.GiftformReviewer.LastName))
                .ForMember(dest => dest.GiftformReviewerId, opt => opt.MapFrom(src => src.GiftformReviewer.Id))
                .ForMember(dest => dest.CurrentDesignationGuid, opt => opt.MapFrom(src => src.CurrentDesignation.DesignationGuid))
                .ForMember(dest => dest.CurrentDesignation, opt => opt.MapFrom(src => src.CurrentDesignation.Designation))
                .ForMember(dest => dest.DesiredDesignationGuid, opt => opt.MapFrom(src => src.DesiredDesignation.DesignationGuid))
                .ForMember(dest => dest.DesiredDesignation, opt => opt.MapFrom(src => src.DesiredDesignation.Designation))
                .ForMember(dest => dest.CandidateName, opt => opt.MapFrom(src => src.Candidate.FirstName + " " + src.Candidate.LastName))
                .ForMember(dest => dest.CandidateGuid, opt => opt.MapFrom(src => src.Candidate.Id));

            CreateMap<GiftProgramModifyDTO, CndGiftform>()
            .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));


            CreateMap<NominationDTO, NmsNomination>()
             .ForMember(dest => dest.NominationGuid, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<NmsNomination, NominationDisplayDTO>()
                .ForMember(dest => dest.NominatorName, opt => opt.MapFrom(src => src.Nominator.FirstName + " " + src.Nominator.LastName))
                .ForMember(dest => dest.NomineeName, opt => opt.MapFrom(src => src.Nominee.FirstName + " " + src.Nominee.LastName))
                .ForMember(dest => dest.InternalProgramTitle, opt => opt.MapFrom(src => src.InternalProgram.InternalProgramName))
                .ForMember(dest => dest.InternalProgramCategory, opt => opt.MapFrom(src => src.InternalProgramCategory.InternalProgramCategory));

            CreateMap<NominationModifyDTO, NmsNomination>()
            .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UsrComment, CommentListDTO>()
                .ForMember(dest => dest.CommenterGuid, opt => opt.MapFrom(src => src.Commenter.Id))
                .ForMember(dest => dest.CommenterName, opt => opt.MapFrom(src => src.Commenter.FirstName + " " + src.Commenter.LastName))
                .ForMember(dest => dest.CommenterProfilePhotoName, opt => opt.MapFrom(src => src.Commenter.ProfilePhotoName));

            CreateMap<CommentEditDTO, UsrComment>()
                .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<BusinessUnitCreateEditDTO, MstBusinessUnit>()
                .ForMember(dest => dest.BusinessUnitGuid, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<MstBusinessUnit, BusinessUnitListDTO>();

            CreateMap<MstPronoun, PronounDisplayDTO>();

            CreateMap<PronounCreateEditDTO, MstPronoun>()
                .ForMember(dest => dest.PronounGuid, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Pronoun, opt => opt.MapFrom(src => src.Pronoun))
                .ForMember(dest => dest.PronounDescription, opt => opt.MapFrom(src => src.PronounDescription))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<MstInternalProgram, InternalProgramViewDTO>();

            CreateMap<InternalProgramModifyDTO, MstInternalProgram>()
                .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<InternalProgramCreateDTO, MstInternalProgram>()
                .ForMember(dest => dest.InternalProgramGuid, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ModificationDate, otp => otp.MapFrom(src => DateTime.UtcNow));

            CreateMap<MstInternalProgramCategory, InternalProgramCategoryViewDTO>()
                .ForMember(dest => dest.InternalProgramGuid, opt => opt.MapFrom(src => src.InternalProgram.InternalProgramGuid))
                .ForMember(dest => dest.InternalProgramName, opt => opt.MapFrom(src => src.InternalProgram.InternalProgramName));

            CreateMap<InternalProgramCategoryCreateDTO, MstInternalProgramCategory>()
                .ForMember(dest => dest.InternalProgramCategoryGuid, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<InternalProgramCategoryModifyDTO, MstInternalProgramCategory>()
                .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<MstProficiency, ProficiencyViewDTO>();

            CreateMap<ProficiencyCreateDTO, MstProficiency>()
                .ForMember(dest => dest.ProficiencyGuid, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.ProficiencyTitle, opt => opt.MapFrom(src => src.ProficiencyTitle))
                .ForMember(dest => dest.ProficiencyDescription, opt => opt.MapFrom(src => src.ProficiencyDescription))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<ProficiencyEditDTO, MstProficiency>()
                .ForMember(dest => dest.ProficiencyTitle, opt => opt.MapFrom(src => src.ProficiencyTitle))
                .ForMember(dest => dest.ProficiencyDescription, opt => opt.MapFrom(src => src.ProficiencyDescription))
                .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UsrCertificateModifyDTO, UsrCertification>()
                .ForMember(dest => dest.CertificationGuid, opt => opt.MapFrom(src => src.UserCertificationGuid))
                .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UsrCertificateAddDTO, UsrCertification>()
                .ForMember(dest => dest.CertificationGuid, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UsrCertification, UsrCertificateViewDTO>()
                .ForMember(dest => dest.UserCertificationGuid, opt => opt.MapFrom(src => src.CertificationGuid))
                .ForMember(dest => dest.CertificationTitle, opt => opt.MapFrom(src => src.Certification.CertificationTitle))
                .ForMember(dest => dest.CertificationGuid, opt => opt.MapFrom(src => src.Certification.CertificationGuid))
                .ForMember(dest => dest.CompanyGuid, opt => opt.MapFrom(src => src.CertificateIssuingCompany.CompanyGuid))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CertificateIssuingCompany.CompanyName));
            //.ForMember(dest => dest.SkillTitle, opt => opt.MapFrom(src => src.Skill.Skill.SkillTitle))
            //.ForMember(dest => dest.SkillGuid, opt => opt.MapFrom(src => src.Skill.UserSkillGuid));

            CreateMap<UsrUser, UsrBasicDetailViewDTO>()
                .ForMember(dest => dest.PronounGuid, opt => opt.MapFrom(src => src.Pronoun.PronounGuid))
                .ForMember(dest => dest.Pronoun, opt => opt.MapFrom(src => src.Pronoun.Pronoun))
                .ForMember(dest => dest.BusinessUnitGuid, opt => opt.MapFrom(src => src.BusinessUnit.BusinessUnitGuid))
                .ForMember(dest => dest.BusinessUnitLogoName, opt => opt.MapFrom(src => src.BusinessUnit.BusinessUnitLogoName))
                .ForMember(dest => dest.BusinessUnitName, opt => opt.MapFrom(src => src.BusinessUnit.BusinessUnitName))
                .ForMember(dest => dest.DesignationGuid, opt => opt.MapFrom(src => src.Designation.DesignationGuid))
                .ForMember(dest => dest.Designation, opt => opt.MapFrom(src => src.Designation.Designation))
                .ForMember(dest => dest.JoiningDate, opt => opt.MapFrom(src => src.CreationDate));

            CreateMap<UsrBasicDetailModifyDTO, UsrUser>()
               .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));



            CreateMap<UsrEducationDTO, UsrEducation>()
                .ForMember(dest => dest.EducationGuid, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UsrEducation, UsrEducationViewDTO>().ReverseMap();

            CreateMap<UsrEducationModifyDTO, UsrEducation>()
               .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UsrExperienceDTO, UsrExperience>()
                .ForMember(dest => dest.ExperienceGuid, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UsrExperience, UsrExperienceViewDTO>()
            .ForMember(dest => dest.EmploymentTypeGuid, opt => opt.MapFrom(src => src.EmploymentType.EmploymentTypeGuid))
            .ForMember(dest => dest.EmploymentTypeName, opt => opt.MapFrom(src => src.EmploymentType.EmploymentTypeTitle))
            .ForMember(dest => dest.LocationTypeGuid, opt => opt.MapFrom(src => src.LocationType.LocationTypeGuid))
            .ForMember(dest => dest.LocationTypeName, opt => opt.MapFrom(src => src.LocationType.LocationType))
            .ForMember(dest => dest.CompanyGuid, opt => opt.MapFrom(src => src.Company.CompanyGuid))
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.CompanyName))
            .ForMember(dest => dest.DesignationGuid, opt => opt.MapFrom(src => src.Designation.DesignationGuid))
            .ForMember(dest => dest.Designation, opt => opt.MapFrom(src => src.Designation.Designation));

            CreateMap<UsrExperienceModifyDTO, UsrExperience>()
               .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UsrTrainingDTO, UsrTraining>()
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UsrTrainingModifyDTO, UsrTraining>()
               .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UsrTraining, UsrTrainingDisplayDTO>()
                   .ForMember(dest => dest.TrainingType, opt => opt.MapFrom(src => src.TrainingType.TrainingType))
                   .ForMember(dest => dest.TrainingTypeGuid, opt => opt.MapFrom(src => src.TrainingType.TrainingTypeGuid))
                   .ForMember(dest => dest.TrainingTitle, opt => opt.MapFrom(src => src.Training.TrainingTitle))
                   .ForMember(dest => dest.TrainingNameGuid, opt => opt.MapFrom(src => src.Training.TrainingGuid));

            CreateMap<UsrSkillDTO, UsrSkill>()
                .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UsrSkill, UsrSkillDisplayDTO>()
                .ForMember(dest => dest.SkillTitle, opt => opt.MapFrom(src => src.Skill.SkillTitle))
                .ForMember(dest => dest.SkillGuid, opt => opt.MapFrom(src => src.Skill.SkillGuid));

            CreateMap<UsrProjectDTO, UsrProject>()
                .ForMember(dest => dest.UserProjectGuid, opt => opt.MapFrom(src => Guid.NewGuid()))
                 .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                 .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UsrProject, UsrProjectDisplayDTO>()
                .ForMember(dest => dest.ProjectGuid, opt => opt.MapFrom(src => src.Project.ProjectGuid))
                .ForMember(dest => dest.ProjectTitle, opt => opt.MapFrom(src => src.Project.ProjectTitle));

            CreateMap<UsrProjectModifyDTO, UsrProject>()
                 .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UsrLanguageDTO, UsrLanguage>()
                .ForMember(dest => dest.LanguageGuid, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UsrLanguage, UsrLanguageDisplayDTO>()
             .ForMember(dest => dest.ProficiencyGuid, opt => opt.MapFrom(src => src.Proficiency.ProficiencyGuid))
             .ForMember(dest => dest.ProficiencyTitle, opt => opt.MapFrom(src => src.Proficiency.ProficiencyTitle));

            CreateMap<UsrLanguageModifiedDTO, UsrLanguage>()
                 .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UsrKraDTO, UsrKra>()
                .ForMember(dest => dest.Kraguid, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UsrKra, UsrKraViewDTO>();

            CreateMap<UsrKraModifyDTO, UsrKra>()
                 .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<MstDesignation, DesignationViewDTO>()
                .ForMember(dest=>dest.DepartmentGuid, opt=>opt.MapFrom(src=>src.Department.DepartmentGuid))
                .ForMember(dest=>dest.Department, opt=>opt.MapFrom(src=>src.Department.Department));
            CreateMap<DesignationAddDTO, MstDesignation>()
                .ForMember(dest => dest.DesignationGuid, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<DesignationEditDTO, MstDesignation>()
                .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<MstCompany, CompaniesViewDTO>();
            CreateMap<CompanyAddDTO, MstCompany>()
                .ForMember(dest => dest.CompanyGuid, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<CompanyEditDTO, MstCompany>()
                .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<MstProject, ProjectViewDTO>();
            CreateMap<ProjectAddDTO, MstProject>()
                .ForMember(dest => dest.ProjectGuid, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<ProjectEditDTO, MstProject>()
                .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));


            CreateMap<AttachmentDisplayDTO, NmsAttachment>().ReverseMap();

            CreateMap<DepartmentCreateDTO, MstDepartment>()
                .ForMember(dest => dest.DepartmentGuid, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<MstDepartment, DepartmentViewDTO>().ReverseMap();

            CreateMap<DepartmentModifyDTO, MstDepartment>()
                .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<NotificationCreateDTO, MstNotification>()
              .ForMember(dest => dest.NotificationGuid, opt => opt.MapFrom(src => Guid.NewGuid()))
              .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.UtcNow))
              .ForMember(dest => dest.ModificationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<MstNotification, NotificationDisplayDTO>()
               .ForMember(dest => dest.UserGuid, opt => opt.MapFrom(src => src.User.Id));

        }

    }
}
