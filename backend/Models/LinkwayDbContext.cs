using LinkwayAPI.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LinkwayAPI.Models;

public partial class LinkwayDbContext : IdentityDbContext<UsrUser>
{
    public LinkwayDbContext()
    {
    }

    public LinkwayDbContext(DbContextOptions<LinkwayDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CndGiftform> CndGiftforms { get; set; }

    public virtual DbSet<MstBusinessUnit> MstBusinessUnits { get; set; }

    public virtual DbSet<MstCause> MstCauses { get; set; }

    public virtual DbSet<MstCertification> MstCertifications { get; set; }

    public virtual DbSet<MstCompany> MstCompanies { get; set; }

    public virtual DbSet<MstDepartment> MstDepartments { get; set; }

    public virtual DbSet<MstDesignation> MstDesignations { get; set; }

    public virtual DbSet<MstEmploymentType> MstEmploymentTypes { get; set; }

    public virtual DbSet<MstInternalProgram> MstInternalPrograms { get; set; }

    public virtual DbSet<MstInternalProgramCategory> MstInternalProgramCategories { get; set; }

    public virtual DbSet<MstLocationType> MstLocationTypes { get; set; }

    public virtual DbSet<MstNotification> MstNotifications { get; set; }

    public virtual DbSet<MstProficiency> MstProficiencies { get; set; }

    public virtual DbSet<MstProject> MstProjects { get; set; }

    public virtual DbSet<MstPronoun> MstPronouns { get; set; }

    public virtual DbSet<MstSkill> MstSkills { get; set; }

    public virtual DbSet<MstTraining> MstTrainings { get; set; }

    public virtual DbSet<MstTrainingType> MstTrainingTypes { get; set; }

    public virtual DbSet<NmsAttachment> NmsAttachments { get; set; }

    public virtual DbSet<NmsNomination> NmsNominations { get; set; }

    public virtual DbSet<RstRequest> RstRequests { get; set; }

    public virtual DbSet<UsrAward> UsrAwards { get; set; }

    public virtual DbSet<UsrCertification> UsrCertifications { get; set; }

    public virtual DbSet<UsrComment> UsrComments { get; set; }

    public virtual DbSet<UsrEducation> UsrEducations { get; set; }

    public virtual DbSet<UsrExperience> UsrExperiences { get; set; }

    public virtual DbSet<UsrKra> UsrKras { get; set; }

    public virtual DbSet<UsrLanguage> UsrLanguages { get; set; }

    public virtual DbSet<UsrPatent> UsrPatents { get; set; }

    public virtual DbSet<UsrProject> UsrProjects { get; set; }

    public virtual DbSet<UsrSkill> UsrSkills { get; set; }

    public virtual DbSet<UsrTraining> UsrTrainings { get; set; }

    public virtual DbSet<UsrVolunteeringExperience> UsrVolunteeringExperiences { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UsrUser>().ToTable(tb => tb.HasTrigger("TRG_AspNetUsers_Delete"));

        modelBuilder.Entity<CndGiftform>(entity =>
        {
            entity.HasKey(e => e.GiftformId);

            entity.ToTable("CND_GIFTForms");

            entity.HasIndex(e => e.GiftformGuid, "IX_CND_GIFTForms").IsUnique();

            entity.Property(e => e.GiftformId).HasColumnName("GIFTFormId");
            entity.Property(e => e.CareerGrowthContribution).HasMaxLength(1000);
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.EndResult).HasMaxLength(1000);
            entity.Property(e => e.GapsIdentified).HasMaxLength(1000);
            entity.Property(e => e.GiftformAdminStatus).HasColumnName("GIFTFormAdminStatus");
            entity.Property(e => e.GiftformGuid).HasColumnName("GIFTFormGuid");
            entity.Property(e => e.GiftformManagerStatus).HasColumnName("GIFTFormManagerStatus");
            entity.Property(e => e.GiftformRejectionReason)
                .HasMaxLength(500)
                .HasColumnName("GIFTFormRejectionReason");
            entity.Property(e => e.GiftformReviewerId).HasColumnName("GIFTFormReviewerId");
            entity.Property(e => e.LearningAndTransistionProcess).HasMaxLength(1000);
            entity.Property(e => e.MajorAchievements).HasMaxLength(1000);
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");
            entity.Property(e => e.PlanOfAction).HasMaxLength(1000);
            entity.Property(e => e.TargetAchievementDate).HasColumnType("datetime");
            entity.Property(e => e.WorkRelatedTrainingAndCertifications).HasMaxLength(1000);

            entity.HasOne(d => d.Candidate).WithMany(p => p.CndGiftformCandidates)
                .HasPrincipalKey(p => p.EmployeeCode)
                .HasForeignKey(d => d.CandidateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CND_GIFTForms_CND_GIFTForms");

            entity.HasOne(d => d.CurrentDesignation).WithMany(p => p.CndGiftformCurrentDesignations)
                .HasForeignKey(d => d.CurrentDesignationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CND_GIFTForms_MST_Designations");

            entity.HasOne(d => d.DesiredDesignation).WithMany(p => p.CndGiftformDesiredDesignations)
                .HasForeignKey(d => d.DesiredDesignationId)
                .HasConstraintName("FK_CND_GIFTForms_MST_Designations1");

            entity.HasOne(d => d.GiftformReviewer).WithMany(p => p.CndGiftformGiftformReviewers)
                .HasPrincipalKey(p => p.EmployeeCode)
                .HasForeignKey(d => d.GiftformReviewerId)
                .HasConstraintName("FK_CND_GIFTForms_AspNetUsers");
        });

        modelBuilder.Entity<MstBusinessUnit>(entity =>
        {
            entity.HasKey(e => e.BusinessUnitId);

            entity.ToTable("MST_BusinessUnits");

            entity.HasIndex(e => e.BusinessUnitGuid, "IX_MST_BusinessUnits").IsUnique();

            entity.Property(e => e.BusinessUnitDescription).HasMaxLength(500);
            entity.Property(e => e.BusinessUnitLogoName).HasMaxLength(100);
            entity.Property(e => e.BusinessUnitName).HasMaxLength(100);
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<MstCause>(entity =>
        {
            entity.HasKey(e => e.CauseId);

            entity.ToTable("MST_Causes");

            entity.HasIndex(e => e.CauseGuid, "IX_MST_Causes").IsUnique();

            entity.Property(e => e.Cause).HasMaxLength(100);
            entity.Property(e => e.CauseDescription).HasMaxLength(500);
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<MstCertification>(entity =>
        {
            entity.HasKey(e => e.CertificationId);

            entity.ToTable("MST_Certifications");

            entity.Property(e => e.CertificationDescription).HasMaxLength(500);
            entity.Property(e => e.CertificationTitle).HasMaxLength(100);
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<MstCompany>(entity =>
        {
            entity.HasKey(e => e.CompanyId);

            entity.ToTable("MST_Companies");

            entity.HasIndex(e => e.CompanyGuid, "IX_MST_Companies").IsUnique();

            entity.Property(e => e.CompanyDescription).HasMaxLength(500);
            entity.Property(e => e.CompanyName).HasMaxLength(100);
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<MstDepartment>(entity =>
        {
            entity.HasKey(e => e.DepartmentId);

            entity.ToTable("MST_Departments");

            entity.HasIndex(e => e.DepartmentGuid, "IX_MST_Departments").IsUnique();

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Department).HasMaxLength(100);
            entity.Property(e => e.DepartmentDescription).HasMaxLength(500);
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<MstDesignation>(entity =>
        {
            entity.HasKey(e => e.DesignationId);

            entity.ToTable("MST_Designations");

            entity.HasIndex(e => e.DesignationGuid, "IX_MST_Designations").IsUnique();

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Designation).HasMaxLength(100);
            entity.Property(e => e.DesignationDescription).HasMaxLength(500);
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");

            entity.HasOne(d => d.Department).WithMany(p => p.MstDesignations)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_MST_Designations_MST_Departments");
        });

        modelBuilder.Entity<MstEmploymentType>(entity =>
        {
            entity.HasKey(e => e.EmploymentTypeId);

            entity.ToTable("MST_EmploymentTypes");

            entity.HasIndex(e => e.EmploymentTypeGuid, "IX_MST_EmploymentTypes").IsUnique();

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.EmploymentTypeDescription).HasMaxLength(500);
            entity.Property(e => e.EmploymentTypeTitle).HasMaxLength(100);
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<MstInternalProgram>(entity =>
        {
            entity.HasKey(e => e.InternalProgramId);

            entity.ToTable("MST_InternalPrograms");

            entity.HasIndex(e => e.InternalProgramGuid, "IX_MST_InternalPrograms").IsUnique();

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.InternalProgramDescription).HasMaxLength(500);
            entity.Property(e => e.InternalProgramName).HasMaxLength(100);
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<MstInternalProgramCategory>(entity =>
        {
            entity.HasKey(e => e.InternalProgramCategoryId);

            entity.ToTable("MST_InternalProgramCategories");

            entity.HasIndex(e => e.InternalProgramCategoryGuid, "IX_MST_InternalProgramCategories").IsUnique();

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.InternalProgramCategory).HasMaxLength(100);
            entity.Property(e => e.InternalProgramCategoryDescription).HasMaxLength(500);
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");

            entity.HasOne(d => d.InternalProgram).WithMany(p => p.MstInternalProgramCategories)
                .HasForeignKey(d => d.InternalProgramId)
                .HasConstraintName("FK_MST_InternalProgramCategories_MST_InternalPrograms");
        });

        modelBuilder.Entity<MstLocationType>(entity =>
        {
            entity.HasKey(e => e.LocationTypeId);

            entity.ToTable("MST_LocationTypes");

            entity.HasIndex(e => e.LocationTypeGuid, "IX_MST_LocationTypes").IsUnique();

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.LocationType).HasMaxLength(50);
            entity.Property(e => e.LocationTypeDescription).HasMaxLength(500);
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<MstNotification>(entity =>
        {
            entity.HasKey(e => e.NotificationId);

            entity.ToTable("MST_Notifications");

            entity.HasIndex(e => e.NotificationGuid, "IX_MST_Notifications").IsUnique();

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.IsNotificationRead).HasColumnName("isNotificationRead");
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");
            entity.Property(e => e.NotificationDescription).HasMaxLength(250);
            entity.Property(e => e.NotificationTitle).HasMaxLength(100);

            entity.HasOne(d => d.User).WithMany(p => p.MstNotifications)
                .HasPrincipalKey(p => p.EmployeeCode)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_MST_Notifications_AspNetUsers");
        });

        modelBuilder.Entity<MstProficiency>(entity =>
        {
            entity.HasKey(e => e.ProficiencyId);

            entity.ToTable("MST_Proficiencies");

            entity.HasIndex(e => e.ProficiencyGuid, "IX_MST_Proficiencies").IsUnique();

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");
            entity.Property(e => e.ProficiencyDescription).HasMaxLength(500);
            entity.Property(e => e.ProficiencyTitle).HasMaxLength(100);
        });

        modelBuilder.Entity<MstProject>(entity =>
        {
            entity.HasKey(e => e.ProjectId);

            entity.ToTable("MST_Projects");

            entity.HasIndex(e => e.ProjectGuid, "IX_MST_Projects").IsUnique();

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");
            entity.Property(e => e.ProjectDescription).HasMaxLength(500);
            entity.Property(e => e.ProjectTitle).HasMaxLength(100);
        });

        modelBuilder.Entity<MstPronoun>(entity =>
        {
            entity.HasKey(e => e.PronounId).HasName("PK_MST_Pronounses");

            entity.ToTable("MST_Pronouns");

            entity.HasIndex(e => e.PronounGuid, "IX_MST_Pronouns").IsUnique();

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");
            entity.Property(e => e.Pronoun).HasMaxLength(50);
            entity.Property(e => e.PronounDescription).HasMaxLength(500);
        });

        modelBuilder.Entity<MstSkill>(entity =>
        {
            entity.HasKey(e => e.SkillId);

            entity.ToTable("MST_Skills");

            entity.HasIndex(e => e.SkillGuid, "IX_MST_Skills").IsUnique();

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");
            entity.Property(e => e.SkillDescription).HasMaxLength(500);
            entity.Property(e => e.SkillTitle).HasMaxLength(100);
        });

        modelBuilder.Entity<MstTraining>(entity =>
        {
            entity.HasKey(e => e.TrainingId);

            entity.ToTable("MST_Trainings");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");
            entity.Property(e => e.TrainingDescription).HasMaxLength(500);
            entity.Property(e => e.TrainingTitle).HasMaxLength(100);
        });

        modelBuilder.Entity<MstTrainingType>(entity =>
        {
            entity.HasKey(e => e.TrainingTypeId);

            entity.ToTable("MST_TrainingTypes");

            entity.HasIndex(e => e.TrainingTypeGuid, "IX_MST_TrainingTypes").IsUnique();

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");
            entity.Property(e => e.TrainingType).HasMaxLength(100);
            entity.Property(e => e.TrainingTypeDescription).HasMaxLength(500);
        });

        modelBuilder.Entity<NmsAttachment>(entity =>
        {
            entity.HasKey(e => e.AttachmentId);

            entity.ToTable("NMS_Attachments");

            entity.HasIndex(e => e.AttachmentGuid, "IX_NMS_Attachments").IsUnique();

            entity.Property(e => e.AttachmentName).HasMaxLength(500);
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");

            entity.HasOne(d => d.Nomination).WithMany(p => p.NmsAttachments)
                .HasForeignKey(d => d.NominationId)
                .HasConstraintName("FK_NMS_Attachments_NMS_Nominations");
        });

        modelBuilder.Entity<NmsNomination>(entity =>
        {
            entity.HasKey(e => e.NominationId);

            entity.ToTable("NMS_Nominations");

            entity.HasIndex(e => e.NominationGuid, "IX_NMS_Nominations").IsUnique();

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.JustificationComment).HasMaxLength(500);
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");
            entity.Property(e => e.NominationRejectionReason).HasMaxLength(500);

            entity.HasOne(d => d.InternalProgramCategory).WithMany(p => p.NmsNominations)
                .HasForeignKey(d => d.InternalProgramCategoryId)
                .HasConstraintName("FK_NMS_Nominations_MST_InternalProgramCategories");

            entity.HasOne(d => d.InternalProgram).WithMany(p => p.NmsNominations)
                .HasForeignKey(d => d.InternalProgramId)
                .HasConstraintName("FK_NMS_Nominations_MST_InternalPrograms");

            entity.HasOne(d => d.Nominator).WithMany(p => p.NmsNominationNominators)
                .HasPrincipalKey(p => p.EmployeeCode)
                .HasForeignKey(d => d.NominatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NMS_Nominations_AspNetUsers");

            entity.HasOne(d => d.Nominee).WithMany(p => p.NmsNominationNominees)
                .HasPrincipalKey(p => p.EmployeeCode)
                .HasForeignKey(d => d.NomineeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NMS_Nominations_AspNetUsersNominee");
        });

        modelBuilder.Entity<RstRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK_RST_Request");

            entity.ToTable("RST_Requests");

            entity.HasIndex(e => e.RequestGuid, "IX_RST_Requests").IsUnique();

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");
            entity.Property(e => e.RequestDate).HasColumnType("datetime");
            entity.Property(e => e.RequestDescription).HasMaxLength(500);
            entity.Property(e => e.RequestRejectionReason).HasMaxLength(500);

            entity.HasOne(d => d.Requester).WithMany(p => p.RstRequests)
                .HasPrincipalKey(p => p.EmployeeCode)
                .HasForeignKey(d => d.RequesterId)
                .HasConstraintName("FK_RST_Requests_AspNetUsers");

            entity.HasMany(d => d.Users).WithMany(p => p.Requests)
                .UsingEntity<Dictionary<string, object>>(
                    "RelRequestsUser",
                    r => r.HasOne<UsrUser>().WithMany()
                        .HasPrincipalKey("EmployeeCode")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_REL_RequestsUsers_AspNetUsers"),
                    l => l.HasOne<RstRequest>().WithMany()
                        .HasForeignKey("RequestId")
                        .HasConstraintName("FK_REL_RequestsUsers_RST_Requests"),
                    j =>
                    {
                        j.HasKey("RequestId", "UserId");
                        j.ToTable("REL_RequestsUsers");
                    });
        });

        modelBuilder.Entity<UsrAward>(entity =>
        {
            entity.HasKey(e => e.AwardId);

            entity.ToTable("USR_Awards");

            entity.HasIndex(e => e.AwardGuid, "IX_USR_Awards").IsUnique();

            entity.Property(e => e.AwardDescription).HasMaxLength(500);
            entity.Property(e => e.AwardIssueDate).HasColumnType("datetime");
            entity.Property(e => e.AwardIssuer).HasMaxLength(100);
            entity.Property(e => e.AwardPhotoName).HasMaxLength(500);
            entity.Property(e => e.AwardTitle).HasMaxLength(100);
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.UsrAwards)
                .HasPrincipalKey(p => p.EmployeeCode)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_USR_Awards_AspNetUsers");
        });

        modelBuilder.Entity<UsrCertification>(entity =>
        {
            entity.HasKey(e => e.UserCertificationId).HasName("PK_EMP_Certificates");

            entity.ToTable("USR_Certifications");

            entity.HasIndex(e => e.CertificationGuid, "IX_USR_Certificates").IsUnique();

            entity.Property(e => e.CertificationCredentialId).HasMaxLength(250);
            entity.Property(e => e.CertificationCredentialUrl)
                .HasMaxLength(500)
                .HasColumnName("CertificationCredentialURL");
            entity.Property(e => e.CertificationDescription).HasMaxLength(500);
            entity.Property(e => e.CertificationExpirationDate).HasColumnType("datetime");
            entity.Property(e => e.CertificationIssueDate).HasColumnType("datetime");
            entity.Property(e => e.CertificationPhotoName).HasMaxLength(500);
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");

            entity.HasOne(d => d.CertificateIssuingCompany).WithMany(p => p.UsrCertifications)
                .HasForeignKey(d => d.CertificateIssuingCompanyId)
                .HasConstraintName("FK_USR_Certifications_MST_Companies");

            entity.HasOne(d => d.Certification).WithMany(p => p.UsrCertifications)
                .HasForeignKey(d => d.CertificationId)
                .HasConstraintName("FK_USR_Certifications_MST_Certifications");

            entity.HasOne(d => d.User).WithMany(p => p.UsrCertifications)
                .HasPrincipalKey(p => p.EmployeeCode)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_USR_Certifications_AspNetUsers");

            entity.HasMany(d => d.Skills).WithMany(p => p.Certifications)
                .UsingEntity<Dictionary<string, object>>(
                    "RelCertificationsSkill",
                    r => r.HasOne<MstSkill>().WithMany()
                        .HasForeignKey("SkillId")
                        .HasConstraintName("FK_REL_CertificationsSkills_MST_Skills"),
                    l => l.HasOne<UsrCertification>().WithMany()
                        .HasForeignKey("CertificationId")
                        .HasConstraintName("FK_REL_CertificationsSkills_USR_Certifications"),
                    j =>
                    {
                        j.HasKey("CertificationId", "SkillId").HasName("PK_REL_CertificationsSkills_1");
                        j.ToTable("REL_CertificationsSkills");
                    });
        });

        modelBuilder.Entity<UsrComment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK_EMP_Comments");

            entity.ToTable("USR_Comments");

            entity.HasIndex(e => e.CommentGuid, "IX_USR_Comments").IsUnique();

            entity.Property(e => e.Comment).HasMaxLength(500);
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");

            entity.HasOne(d => d.Commenter).WithMany(p => p.UsrCommentCommenters)
                .HasPrincipalKey(p => p.EmployeeCode)
                .HasForeignKey(d => d.CommenterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USR_Comments_AspNetUsers");

            entity.HasOne(d => d.User).WithMany(p => p.UsrCommentUsers)
                .HasPrincipalKey(p => p.EmployeeCode)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USR_Comments_AspNetUsersEmployee");
        });

        modelBuilder.Entity<UsrEducation>(entity =>
        {
            entity.HasKey(e => e.EducationId);

            entity.ToTable("USR_Educations");

            entity.HasIndex(e => e.EducationGuid, "IX_USR_Educations").IsUnique();

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.EducationDegree).HasMaxLength(100);
            entity.Property(e => e.EducationDescription).HasMaxLength(500);
            entity.Property(e => e.EducationEndDate).HasColumnType("datetime");
            entity.Property(e => e.EducationGrade).HasMaxLength(50);
            entity.Property(e => e.EducationInstituteName).HasMaxLength(100);
            entity.Property(e => e.EducationStartDate).HasColumnType("datetime");
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.UsrEducations)
                .HasPrincipalKey(p => p.EmployeeCode)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_USR_Educations_AspNetUsers");
        });

        modelBuilder.Entity<UsrExperience>(entity =>
        {
            entity.HasKey(e => e.ExperienceId).HasName("PK_EMP_Experience");

            entity.ToTable("USR_Experiences");

            entity.HasIndex(e => e.ExperienceGuid, "IX_USR_Experiences").IsUnique();

            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.ExperienceDescription).HasMaxLength(500);
            entity.Property(e => e.ExperienceEndDate).HasColumnType("datetime");
            entity.Property(e => e.ExperienceStartDate).HasColumnType("datetime");
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");
            entity.Property(e => e.State).HasMaxLength(100);

            entity.HasOne(d => d.Company).WithMany(p => p.UsrExperiences)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK_USR_Experiences_MST_Companies");

            entity.HasOne(d => d.Designation).WithMany(p => p.UsrExperiences)
                .HasForeignKey(d => d.DesignationId)
                .HasConstraintName("FK_USR_Experiences_MST_Designations");

            entity.HasOne(d => d.EmploymentType).WithMany(p => p.UsrExperiences)
                .HasForeignKey(d => d.EmploymentTypeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_EMP_Experience_MST_EmploymentTypes");

            entity.HasOne(d => d.LocationType).WithMany(p => p.UsrExperiences)
                .HasForeignKey(d => d.LocationTypeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_USR_Experiences_MST_LocationTypes");

            entity.HasOne(d => d.User).WithMany(p => p.UsrExperiences)
                .HasPrincipalKey(p => p.EmployeeCode)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_USR_Experiences_AspNetUsers");
        });

        modelBuilder.Entity<UsrKra>(entity =>
        {
            entity.HasKey(e => e.Kraid);

            entity.ToTable("USR_KRAs");

            entity.HasIndex(e => e.Kraguid, "IX_USR_KRAs").IsUnique();

            entity.Property(e => e.Kraid).HasColumnName("KRAId");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Kradescription)
                .HasMaxLength(500)
                .HasColumnName("KRADescription");
            entity.Property(e => e.Kraguid).HasColumnName("KRAGuid");
            entity.Property(e => e.Kratitle)
                .HasMaxLength(100)
                .HasColumnName("KRATitle");
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.UsrKras)
                .HasPrincipalKey(p => p.EmployeeCode)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_USR_KRAs_AspNetUsers");
        });

        modelBuilder.Entity<UsrLanguage>(entity =>
        {
            entity.HasKey(e => e.LanguageId);

            entity.ToTable("USR_Languages");

            entity.HasIndex(e => e.LanguageGuid, "IX_USR_Languages").IsUnique();

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.LanguageName).HasMaxLength(50);
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");

            entity.HasOne(d => d.Proficiency).WithMany(p => p.UsrLanguages)
                .HasForeignKey(d => d.ProficiencyId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_USR_Languages_MST_Proficiencies");

            entity.HasOne(d => d.User).WithMany(p => p.UsrLanguages)
                .HasPrincipalKey(p => p.EmployeeCode)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_USR_Languages_AspNetUsers");
        });

        modelBuilder.Entity<UsrPatent>(entity =>
        {
            entity.HasKey(e => e.PatentId);

            entity.ToTable("USR_Patents");

            entity.HasIndex(e => e.PatentGuid, "IX_USR_Patents").IsUnique();

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");
            entity.Property(e => e.PatentApplicationNumber).HasMaxLength(100);
            entity.Property(e => e.PatentDescription).HasMaxLength(500);
            entity.Property(e => e.PatentIssueDate).HasColumnType("datetime");
            entity.Property(e => e.PatentTitle).HasMaxLength(100);
            entity.Property(e => e.PatentUrl)
                .HasMaxLength(500)
                .HasColumnName("PatentURL");

            entity.HasOne(d => d.User).WithMany(p => p.UsrPatents)
                .HasPrincipalKey(p => p.EmployeeCode)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_USR_Patents_AspNetUsers");
        });

        modelBuilder.Entity<UsrProject>(entity =>
        {
            entity.HasKey(e => e.UserProjectId).HasName("PK_EMP_Projects");

            entity.ToTable("USR_Projects");

            entity.HasIndex(e => e.UserProjectGuid, "IX_USR_Projects").IsUnique();

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.IsProjectActive).HasColumnName("isProjectActive");
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");
            entity.Property(e => e.ProjectDescription).HasMaxLength(500);
            entity.Property(e => e.ProjectEndDate).HasColumnType("datetime");
            entity.Property(e => e.ProjectStartDate).HasColumnType("datetime");

            entity.HasOne(d => d.Project).WithMany(p => p.UsrProjects)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_USR_Projects_MST_Projects");

            entity.HasOne(d => d.User).WithMany(p => p.UsrProjects)
                .HasPrincipalKey(p => p.EmployeeCode)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_USR_Projects_AspNetUsers");

            entity.HasMany(d => d.Skills).WithMany(p => p.UserProjects)
                .UsingEntity<Dictionary<string, object>>(
                    "RelUserProjectsSkill",
                    r => r.HasOne<MstSkill>().WithMany()
                        .HasForeignKey("SkillId")
                        .HasConstraintName("FK_REL_UserProjectsSkills_MST_Skills"),
                    l => l.HasOne<UsrProject>().WithMany()
                        .HasForeignKey("UserProjectId")
                        .HasConstraintName("FK_REL_UserProjectsSkills_USR_Projects"),
                    j =>
                    {
                        j.HasKey("UserProjectId", "SkillId");
                        j.ToTable("REL_UserProjectsSkills");
                    });
        });

        modelBuilder.Entity<UsrSkill>(entity =>
        {
            entity.HasKey(e => e.UserSkillId);

            entity.ToTable("USR_Skills");

            entity.HasIndex(e => e.UserSkillGuid, "IX_USR_Skills").IsUnique();

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");

            entity.HasOne(d => d.Skill).WithMany(p => p.UsrSkills)
                .HasForeignKey(d => d.SkillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USR_Skills_MST_Skills");

            entity.HasOne(d => d.User).WithMany(p => p.UsrSkills)
                .HasPrincipalKey(p => p.EmployeeCode)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_USR_Skills_AspNetUsers");
        });

        modelBuilder.Entity<UsrTraining>(entity =>
        {
            entity.HasKey(e => e.UserTrainingId).HasName("PK_EMP_Trainings");

            entity.ToTable("USR_Trainings");

            entity.HasIndex(e => e.UserTrainingGuid, "IX_USR_Trainings").IsUnique();

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");
            entity.Property(e => e.TrainingEndDate).HasColumnType("datetime");
            entity.Property(e => e.TrainingStartDate).HasColumnType("datetime");
            entity.Property(e => e.UserTrainingDescription).HasMaxLength(500);

            entity.HasOne(d => d.Training).WithMany(p => p.UsrTrainings)
                .HasForeignKey(d => d.TrainingId)
                .HasConstraintName("FK_USR_Trainings_MST_Trainings");

            entity.HasOne(d => d.TrainingType).WithMany(p => p.UsrTrainings)
                .HasForeignKey(d => d.TrainingTypeId)
                .HasConstraintName("FK_EMP_Trainings_MST_TrainingTypes");

            entity.HasOne(d => d.User).WithMany(p => p.UsrTrainings)
                .HasPrincipalKey(p => p.EmployeeCode)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_USR_Trainings_AspNetUsers");
        });

        modelBuilder.Entity<UsrVolunteeringExperience>(entity =>
        {
            entity.HasKey(e => e.VolunteeringExperienceId);

            entity.ToTable("USR_VolunteeringExperiences");

            entity.HasIndex(e => e.VolunteeringExperienceGuid, "IX_USR_VolunteeringExperiences").IsUnique();

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");
            entity.Property(e => e.VolunteeringExperienceDescription).HasMaxLength(500);
            entity.Property(e => e.VolunteeringExperienceEndDate).HasColumnType("datetime");
            entity.Property(e => e.VolunteeringExperienceOrganization).HasMaxLength(100);
            entity.Property(e => e.VolunteeringExperienceRole).HasMaxLength(100);
            entity.Property(e => e.VolunteeringExperienceStartDate).HasColumnType("datetime");

            entity.HasOne(d => d.Cause).WithMany(p => p.UsrVolunteeringExperiences)
                .HasForeignKey(d => d.CauseId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_USR_VolunteeringExperiences_MST_Causes");

            entity.HasOne(d => d.User).WithMany(p => p.UsrVolunteeringExperiences)
                .HasPrincipalKey(p => p.EmployeeCode)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_USR_VolunteeringExperiences_AspNetUsers");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
