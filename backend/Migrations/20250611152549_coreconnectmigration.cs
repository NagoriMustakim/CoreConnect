using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkwayAPI.Migrations
{
    /// <inheritdoc />
    public partial class coreconnectmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "About",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BannerPhotoName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BusinessUnitId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DesignationId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContactNo",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LinkedInUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ProfilePhotoName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PronounId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SkypeId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_AspNetUsers_EmployeeCode",
                table: "AspNetUsers",
                column: "EmployeeCode");

            migrationBuilder.CreateTable(
                name: "MST_BusinessUnits",
                columns: table => new
                {
                    BusinessUnitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessUnitGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BusinessUnitName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BusinessUnitLogoName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BusinessUnitDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MST_BusinessUnits", x => x.BusinessUnitId);
                });

            migrationBuilder.CreateTable(
                name: "MST_Causes",
                columns: table => new
                {
                    CauseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CauseGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cause = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CauseDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MST_Causes", x => x.CauseId);
                });

            migrationBuilder.CreateTable(
                name: "MST_Certifications",
                columns: table => new
                {
                    CertificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CertificationGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CertificationTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CertificationDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MST_Certifications", x => x.CertificationId);
                });

            migrationBuilder.CreateTable(
                name: "MST_Companies",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CompanyDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MST_Companies", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "MST_Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DepartmentDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MST_Departments", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "MST_EmploymentTypes",
                columns: table => new
                {
                    EmploymentTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmploymentTypeGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmploymentTypeTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmploymentTypeDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MST_EmploymentTypes", x => x.EmploymentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "MST_InternalPrograms",
                columns: table => new
                {
                    InternalProgramId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InternalProgramGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InternalProgramName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InternalProgramDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    InternalProgramReviewCycle = table.Column<int>(type: "int", nullable: false),
                    InternalProgramActiveDays = table.Column<int>(type: "int", nullable: false),
                    IsInternalProgramCategoryExists = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MST_InternalPrograms", x => x.InternalProgramId);
                });

            migrationBuilder.CreateTable(
                name: "MST_LocationTypes",
                columns: table => new
                {
                    LocationTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationTypeGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LocationTypeDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MST_LocationTypes", x => x.LocationTypeId);
                });

            migrationBuilder.CreateTable(
                name: "MST_Notifications",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    NotificationTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NotificationDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    isNotificationRead = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MST_Notifications", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_MST_Notifications_AspNetUsers",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "EmployeeCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MST_Proficiencies",
                columns: table => new
                {
                    ProficiencyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProficiencyGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProficiencyTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProficiencyDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MST_Proficiencies", x => x.ProficiencyId);
                });

            migrationBuilder.CreateTable(
                name: "MST_Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProjectDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MST_Projects", x => x.ProjectId);
                });

            migrationBuilder.CreateTable(
                name: "MST_Pronouns",
                columns: table => new
                {
                    PronounId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PronounGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Pronoun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PronounDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MST_Pronounses", x => x.PronounId);
                });

            migrationBuilder.CreateTable(
                name: "MST_Skills",
                columns: table => new
                {
                    SkillId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SkillDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MST_Skills", x => x.SkillId);
                });

            migrationBuilder.CreateTable(
                name: "MST_Trainings",
                columns: table => new
                {
                    TrainingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrainingTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TrainingDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MST_Trainings", x => x.TrainingId);
                });

            migrationBuilder.CreateTable(
                name: "MST_TrainingTypes",
                columns: table => new
                {
                    TrainingTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingTypeGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrainingType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TrainingTypeDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MST_TrainingTypes", x => x.TrainingTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RST_Requests",
                columns: table => new
                {
                    RequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequesterId = table.Column<int>(type: "int", nullable: false),
                    RequestType = table.Column<int>(type: "int", nullable: false),
                    RequestDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RequestStatus = table.Column<int>(type: "int", nullable: false),
                    NoOfMembers = table.Column<int>(type: "int", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    RequestRejectionReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RST_Request", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_RST_Requests_AspNetUsers",
                        column: x => x.RequesterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "EmployeeCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USR_Awards",
                columns: table => new
                {
                    AwardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AwardGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AwardTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AwardIssuer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AwardIssueDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    AwardDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AwardPhotoName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USR_Awards", x => x.AwardId);
                    table.ForeignKey(
                        name: "FK_USR_Awards_AspNetUsers",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "EmployeeCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USR_Comments",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommenterId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMP_Comments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_USR_Comments_AspNetUsers",
                        column: x => x.CommenterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "EmployeeCode");
                    table.ForeignKey(
                        name: "FK_USR_Comments_AspNetUsersEmployee",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "EmployeeCode");
                });

            migrationBuilder.CreateTable(
                name: "USR_Educations",
                columns: table => new
                {
                    EducationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EducationGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    EducationInstituteName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EducationDegree = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EducationStartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EducationEndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsEducationActive = table.Column<bool>(type: "bit", nullable: false),
                    EducationGrade = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EducationDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USR_Educations", x => x.EducationId);
                    table.ForeignKey(
                        name: "FK_USR_Educations_AspNetUsers",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "EmployeeCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USR_KRAs",
                columns: table => new
                {
                    KRAId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KRAGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    KRATitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    KRADescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USR_KRAs", x => x.KRAId);
                    table.ForeignKey(
                        name: "FK_USR_KRAs_AspNetUsers",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "EmployeeCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USR_Patents",
                columns: table => new
                {
                    PatentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatentGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PatentTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PatentApplicationNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PatentStatus = table.Column<int>(type: "int", nullable: false),
                    PatentIssueDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    PatentURL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PatentDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USR_Patents", x => x.PatentId);
                    table.ForeignKey(
                        name: "FK_USR_Patents_AspNetUsers",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "EmployeeCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USR_VolunteeringExperiences",
                columns: table => new
                {
                    VolunteeringExperienceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VolunteeringExperienceGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CauseId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    VolunteeringExperienceOrganization = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VolunteeringExperienceRole = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VolunteeringExperienceStartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    VolunteeringExperienceEndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsVolunteeringExperienceActive = table.Column<bool>(type: "bit", nullable: false),
                    VolunteeringExperienceDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USR_VolunteeringExperiences", x => x.VolunteeringExperienceId);
                    table.ForeignKey(
                        name: "FK_USR_VolunteeringExperiences_AspNetUsers",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "EmployeeCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USR_VolunteeringExperiences_MST_Causes",
                        column: x => x.CauseId,
                        principalTable: "MST_Causes",
                        principalColumn: "CauseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USR_Certifications",
                columns: table => new
                {
                    UserCertificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CertificationGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CertificationId = table.Column<int>(type: "int", nullable: false),
                    CertificateIssuingCompanyId = table.Column<int>(type: "int", nullable: false),
                    CertificationIssueDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CertificationExpirationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CertificationCredentialId = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CertificationCredentialURL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CertificationPhotoName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CertificationDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMP_Certificates", x => x.UserCertificationId);
                    table.ForeignKey(
                        name: "FK_USR_Certifications_AspNetUsers",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "EmployeeCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USR_Certifications_MST_Certifications",
                        column: x => x.CertificationId,
                        principalTable: "MST_Certifications",
                        principalColumn: "CertificationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USR_Certifications_MST_Companies",
                        column: x => x.CertificateIssuingCompanyId,
                        principalTable: "MST_Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MST_Designations",
                columns: table => new
                {
                    DesignationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DesignationGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DesignationLevel = table.Column<int>(type: "int", nullable: false),
                    DesignationDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MST_Designations", x => x.DesignationId);
                    table.ForeignKey(
                        name: "FK_MST_Designations_MST_Departments",
                        column: x => x.DepartmentId,
                        principalTable: "MST_Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MST_InternalProgramCategories",
                columns: table => new
                {
                    InternalProgramCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InternalProgramCategoryGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InternalProgramId = table.Column<int>(type: "int", nullable: false),
                    InternalProgramCategory = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InternalProgramCategoryDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MST_InternalProgramCategories", x => x.InternalProgramCategoryId);
                    table.ForeignKey(
                        name: "FK_MST_InternalProgramCategories_MST_InternalPrograms",
                        column: x => x.InternalProgramId,
                        principalTable: "MST_InternalPrograms",
                        principalColumn: "InternalProgramId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USR_Languages",
                columns: table => new
                {
                    LanguageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProficiencyId = table.Column<int>(type: "int", nullable: true),
                    LanguageName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USR_Languages", x => x.LanguageId);
                    table.ForeignKey(
                        name: "FK_USR_Languages_AspNetUsers",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "EmployeeCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USR_Languages_MST_Proficiencies",
                        column: x => x.ProficiencyId,
                        principalTable: "MST_Proficiencies",
                        principalColumn: "ProficiencyId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "USR_Projects",
                columns: table => new
                {
                    UserProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserProjectGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    ProjectStartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ProjectEndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    isProjectActive = table.Column<bool>(type: "bit", nullable: false),
                    ProjectDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMP_Projects", x => x.UserProjectId);
                    table.ForeignKey(
                        name: "FK_USR_Projects_AspNetUsers",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "EmployeeCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USR_Projects_MST_Projects",
                        column: x => x.ProjectId,
                        principalTable: "MST_Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USR_Skills",
                columns: table => new
                {
                    UserSkillId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserSkillGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProficiencyId = table.Column<int>(type: "int", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USR_Skills", x => x.UserSkillId);
                    table.ForeignKey(
                        name: "FK_USR_Skills_AspNetUsers",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "EmployeeCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USR_Skills_MST_Skills",
                        column: x => x.SkillId,
                        principalTable: "MST_Skills",
                        principalColumn: "SkillId");
                });

            migrationBuilder.CreateTable(
                name: "USR_Trainings",
                columns: table => new
                {
                    UserTrainingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserTrainingGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrainingTypeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TrainingId = table.Column<int>(type: "int", nullable: false),
                    TrainingStartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TrainingEndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsTrainingActive = table.Column<bool>(type: "bit", nullable: false),
                    UserTrainingDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMP_Trainings", x => x.UserTrainingId);
                    table.ForeignKey(
                        name: "FK_EMP_Trainings_MST_TrainingTypes",
                        column: x => x.TrainingTypeId,
                        principalTable: "MST_TrainingTypes",
                        principalColumn: "TrainingTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USR_Trainings_AspNetUsers",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "EmployeeCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USR_Trainings_MST_Trainings",
                        column: x => x.TrainingId,
                        principalTable: "MST_Trainings",
                        principalColumn: "TrainingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "REL_RequestsUsers",
                columns: table => new
                {
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REL_RequestsUsers", x => new { x.RequestId, x.UserId });
                    table.ForeignKey(
                        name: "FK_REL_RequestsUsers_AspNetUsers",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "EmployeeCode");
                    table.ForeignKey(
                        name: "FK_REL_RequestsUsers_RST_Requests",
                        column: x => x.RequestId,
                        principalTable: "RST_Requests",
                        principalColumn: "RequestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "REL_CertificationsSkills",
                columns: table => new
                {
                    CertificationId = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REL_CertificationsSkills_1", x => new { x.CertificationId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_REL_CertificationsSkills_MST_Skills",
                        column: x => x.SkillId,
                        principalTable: "MST_Skills",
                        principalColumn: "SkillId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_REL_CertificationsSkills_USR_Certifications",
                        column: x => x.CertificationId,
                        principalTable: "USR_Certifications",
                        principalColumn: "UserCertificationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CND_GIFTForms",
                columns: table => new
                {
                    GIFTFormId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GIFTFormGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CandidateId = table.Column<int>(type: "int", nullable: false),
                    GIFTFormReviewerId = table.Column<int>(type: "int", nullable: true),
                    CurrentDesignationId = table.Column<int>(type: "int", nullable: false),
                    DesiredDesignationId = table.Column<int>(type: "int", nullable: false),
                    GapsIdentified = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PlanOfAction = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TargetAchievementDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsSupportRequired = table.Column<bool>(type: "bit", nullable: false),
                    EndResult = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    MajorAchievements = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    LearningAndTransistionProcess = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CareerGrowthContribution = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    WorkRelatedTrainingAndCertifications = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    GIFTFormManagerStatus = table.Column<int>(type: "int", nullable: false),
                    GIFTFormAdminStatus = table.Column<int>(type: "int", nullable: false),
                    GIFTFormRejectionReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CND_GIFTForms", x => x.GIFTFormId);
                    table.ForeignKey(
                        name: "FK_CND_GIFTForms_AspNetUsers",
                        column: x => x.GIFTFormReviewerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "EmployeeCode");
                    table.ForeignKey(
                        name: "FK_CND_GIFTForms_CND_GIFTForms",
                        column: x => x.CandidateId,
                        principalTable: "AspNetUsers",
                        principalColumn: "EmployeeCode");
                    table.ForeignKey(
                        name: "FK_CND_GIFTForms_MST_Designations",
                        column: x => x.CurrentDesignationId,
                        principalTable: "MST_Designations",
                        principalColumn: "DesignationId");
                    table.ForeignKey(
                        name: "FK_CND_GIFTForms_MST_Designations1",
                        column: x => x.DesiredDesignationId,
                        principalTable: "MST_Designations",
                        principalColumn: "DesignationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USR_Experiences",
                columns: table => new
                {
                    ExperienceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExperienceGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    EmploymentTypeId = table.Column<int>(type: "int", nullable: true),
                    LocationTypeId = table.Column<int>(type: "int", nullable: true),
                    DesignationId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ExperienceStartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ExperienceEndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsExperienceActive = table.Column<bool>(type: "bit", nullable: false),
                    ExperienceDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMP_Experience", x => x.ExperienceId);
                    table.ForeignKey(
                        name: "FK_EMP_Experience_MST_EmploymentTypes",
                        column: x => x.EmploymentTypeId,
                        principalTable: "MST_EmploymentTypes",
                        principalColumn: "EmploymentTypeId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_USR_Experiences_AspNetUsers",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "EmployeeCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USR_Experiences_MST_Companies",
                        column: x => x.CompanyId,
                        principalTable: "MST_Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USR_Experiences_MST_Designations",
                        column: x => x.DesignationId,
                        principalTable: "MST_Designations",
                        principalColumn: "DesignationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USR_Experiences_MST_LocationTypes",
                        column: x => x.LocationTypeId,
                        principalTable: "MST_LocationTypes",
                        principalColumn: "LocationTypeId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "NMS_Nominations",
                columns: table => new
                {
                    NominationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NominationGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NominatorId = table.Column<int>(type: "int", nullable: false),
                    NomineeId = table.Column<int>(type: "int", nullable: false),
                    InternalProgramId = table.Column<int>(type: "int", nullable: false),
                    InternalProgramCategoryId = table.Column<int>(type: "int", nullable: true),
                    CycleIteration = table.Column<int>(type: "int", nullable: false),
                    JustificationComment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    NominationStatus = table.Column<int>(type: "int", nullable: false),
                    NominationRejectionReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NMS_Nominations", x => x.NominationId);
                    table.ForeignKey(
                        name: "FK_NMS_Nominations_AspNetUsers",
                        column: x => x.NominatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "EmployeeCode");
                    table.ForeignKey(
                        name: "FK_NMS_Nominations_AspNetUsersNominee",
                        column: x => x.NomineeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "EmployeeCode");
                    table.ForeignKey(
                        name: "FK_NMS_Nominations_MST_InternalProgramCategories",
                        column: x => x.InternalProgramCategoryId,
                        principalTable: "MST_InternalProgramCategories",
                        principalColumn: "InternalProgramCategoryId");
                    table.ForeignKey(
                        name: "FK_NMS_Nominations_MST_InternalPrograms",
                        column: x => x.InternalProgramId,
                        principalTable: "MST_InternalPrograms",
                        principalColumn: "InternalProgramId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "REL_UserProjectsSkills",
                columns: table => new
                {
                    UserProjectId = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REL_UserProjectsSkills", x => new { x.UserProjectId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_REL_UserProjectsSkills_MST_Skills",
                        column: x => x.SkillId,
                        principalTable: "MST_Skills",
                        principalColumn: "SkillId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_REL_UserProjectsSkills_USR_Projects",
                        column: x => x.UserProjectId,
                        principalTable: "USR_Projects",
                        principalColumn: "UserProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NMS_Attachments",
                columns: table => new
                {
                    AttachmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttachmentGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NominationId = table.Column<int>(type: "int", nullable: false),
                    AttachmentName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NMS_Attachments", x => x.AttachmentId);
                    table.ForeignKey(
                        name: "FK_NMS_Attachments_NMS_Nominations",
                        column: x => x.NominationId,
                        principalTable: "NMS_Nominations",
                        principalColumn: "NominationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BusinessUnitId",
                table: "AspNetUsers",
                column: "BusinessUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DesignationId",
                table: "AspNetUsers",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PronounId",
                table: "AspNetUsers",
                column: "PronounId");

            migrationBuilder.CreateIndex(
                name: "IX_CND_GIFTForms",
                table: "CND_GIFTForms",
                column: "GIFTFormGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CND_GIFTForms_CandidateId",
                table: "CND_GIFTForms",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CND_GIFTForms_CurrentDesignationId",
                table: "CND_GIFTForms",
                column: "CurrentDesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_CND_GIFTForms_DesiredDesignationId",
                table: "CND_GIFTForms",
                column: "DesiredDesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_CND_GIFTForms_GIFTFormReviewerId",
                table: "CND_GIFTForms",
                column: "GIFTFormReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_MST_BusinessUnits",
                table: "MST_BusinessUnits",
                column: "BusinessUnitGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MST_Causes",
                table: "MST_Causes",
                column: "CauseGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MST_Companies",
                table: "MST_Companies",
                column: "CompanyGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MST_Departments",
                table: "MST_Departments",
                column: "DepartmentGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MST_Designations",
                table: "MST_Designations",
                column: "DesignationGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MST_Designations_DepartmentId",
                table: "MST_Designations",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_MST_EmploymentTypes",
                table: "MST_EmploymentTypes",
                column: "EmploymentTypeGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MST_InternalProgramCategories",
                table: "MST_InternalProgramCategories",
                column: "InternalProgramCategoryGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MST_InternalProgramCategories_InternalProgramId",
                table: "MST_InternalProgramCategories",
                column: "InternalProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_MST_InternalPrograms",
                table: "MST_InternalPrograms",
                column: "InternalProgramGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MST_LocationTypes",
                table: "MST_LocationTypes",
                column: "LocationTypeGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MST_Notifications",
                table: "MST_Notifications",
                column: "NotificationGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MST_Notifications_UserId",
                table: "MST_Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MST_Proficiencies",
                table: "MST_Proficiencies",
                column: "ProficiencyGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MST_Projects",
                table: "MST_Projects",
                column: "ProjectGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MST_Pronouns",
                table: "MST_Pronouns",
                column: "PronounGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MST_Skills",
                table: "MST_Skills",
                column: "SkillGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MST_TrainingTypes",
                table: "MST_TrainingTypes",
                column: "TrainingTypeGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NMS_Attachments",
                table: "NMS_Attachments",
                column: "AttachmentGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NMS_Attachments_NominationId",
                table: "NMS_Attachments",
                column: "NominationId");

            migrationBuilder.CreateIndex(
                name: "IX_NMS_Nominations",
                table: "NMS_Nominations",
                column: "NominationGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NMS_Nominations_InternalProgramCategoryId",
                table: "NMS_Nominations",
                column: "InternalProgramCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_NMS_Nominations_InternalProgramId",
                table: "NMS_Nominations",
                column: "InternalProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_NMS_Nominations_NominatorId",
                table: "NMS_Nominations",
                column: "NominatorId");

            migrationBuilder.CreateIndex(
                name: "IX_NMS_Nominations_NomineeId",
                table: "NMS_Nominations",
                column: "NomineeId");

            migrationBuilder.CreateIndex(
                name: "IX_REL_CertificationsSkills_SkillId",
                table: "REL_CertificationsSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_REL_RequestsUsers_UserId",
                table: "REL_RequestsUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_REL_UserProjectsSkills_SkillId",
                table: "REL_UserProjectsSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_RST_Requests",
                table: "RST_Requests",
                column: "RequestGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RST_Requests_RequesterId",
                table: "RST_Requests",
                column: "RequesterId");

            migrationBuilder.CreateIndex(
                name: "IX_USR_Awards",
                table: "USR_Awards",
                column: "AwardGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USR_Awards_UserId",
                table: "USR_Awards",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_USR_Certificates",
                table: "USR_Certifications",
                column: "CertificationGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USR_Certifications_CertificateIssuingCompanyId",
                table: "USR_Certifications",
                column: "CertificateIssuingCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_USR_Certifications_CertificationId",
                table: "USR_Certifications",
                column: "CertificationId");

            migrationBuilder.CreateIndex(
                name: "IX_USR_Certifications_UserId",
                table: "USR_Certifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_USR_Comments",
                table: "USR_Comments",
                column: "CommentGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USR_Comments_CommenterId",
                table: "USR_Comments",
                column: "CommenterId");

            migrationBuilder.CreateIndex(
                name: "IX_USR_Comments_UserId",
                table: "USR_Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_USR_Educations",
                table: "USR_Educations",
                column: "EducationGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USR_Educations_UserId",
                table: "USR_Educations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_USR_Experiences",
                table: "USR_Experiences",
                column: "ExperienceGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USR_Experiences_CompanyId",
                table: "USR_Experiences",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_USR_Experiences_DesignationId",
                table: "USR_Experiences",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_USR_Experiences_EmploymentTypeId",
                table: "USR_Experiences",
                column: "EmploymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_USR_Experiences_LocationTypeId",
                table: "USR_Experiences",
                column: "LocationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_USR_Experiences_UserId",
                table: "USR_Experiences",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_USR_KRAs",
                table: "USR_KRAs",
                column: "KRAGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USR_KRAs_UserId",
                table: "USR_KRAs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_USR_Languages",
                table: "USR_Languages",
                column: "LanguageGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USR_Languages_ProficiencyId",
                table: "USR_Languages",
                column: "ProficiencyId");

            migrationBuilder.CreateIndex(
                name: "IX_USR_Languages_UserId",
                table: "USR_Languages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_USR_Patents",
                table: "USR_Patents",
                column: "PatentGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USR_Patents_UserId",
                table: "USR_Patents",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_USR_Projects",
                table: "USR_Projects",
                column: "UserProjectGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USR_Projects_ProjectId",
                table: "USR_Projects",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_USR_Projects_UserId",
                table: "USR_Projects",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_USR_Skills",
                table: "USR_Skills",
                column: "UserSkillGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USR_Skills_SkillId",
                table: "USR_Skills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_USR_Skills_UserId",
                table: "USR_Skills",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_USR_Trainings",
                table: "USR_Trainings",
                column: "UserTrainingGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USR_Trainings_TrainingId",
                table: "USR_Trainings",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_USR_Trainings_TrainingTypeId",
                table: "USR_Trainings",
                column: "TrainingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_USR_Trainings_UserId",
                table: "USR_Trainings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_USR_VolunteeringExperiences",
                table: "USR_VolunteeringExperiences",
                column: "VolunteeringExperienceGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USR_VolunteeringExperiences_CauseId",
                table: "USR_VolunteeringExperiences",
                column: "CauseId");

            migrationBuilder.CreateIndex(
                name: "IX_USR_VolunteeringExperiences_UserId",
                table: "USR_VolunteeringExperiences",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_MST_BusinessUnits_BusinessUnitId",
                table: "AspNetUsers",
                column: "BusinessUnitId",
                principalTable: "MST_BusinessUnits",
                principalColumn: "BusinessUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_MST_Designations_DesignationId",
                table: "AspNetUsers",
                column: "DesignationId",
                principalTable: "MST_Designations",
                principalColumn: "DesignationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_MST_Pronouns_PronounId",
                table: "AspNetUsers",
                column: "PronounId",
                principalTable: "MST_Pronouns",
                principalColumn: "PronounId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_MST_BusinessUnits_BusinessUnitId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_MST_Designations_DesignationId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_MST_Pronouns_PronounId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CND_GIFTForms");

            migrationBuilder.DropTable(
                name: "MST_BusinessUnits");

            migrationBuilder.DropTable(
                name: "MST_Notifications");

            migrationBuilder.DropTable(
                name: "MST_Pronouns");

            migrationBuilder.DropTable(
                name: "NMS_Attachments");

            migrationBuilder.DropTable(
                name: "REL_CertificationsSkills");

            migrationBuilder.DropTable(
                name: "REL_RequestsUsers");

            migrationBuilder.DropTable(
                name: "REL_UserProjectsSkills");

            migrationBuilder.DropTable(
                name: "USR_Awards");

            migrationBuilder.DropTable(
                name: "USR_Comments");

            migrationBuilder.DropTable(
                name: "USR_Educations");

            migrationBuilder.DropTable(
                name: "USR_Experiences");

            migrationBuilder.DropTable(
                name: "USR_KRAs");

            migrationBuilder.DropTable(
                name: "USR_Languages");

            migrationBuilder.DropTable(
                name: "USR_Patents");

            migrationBuilder.DropTable(
                name: "USR_Skills");

            migrationBuilder.DropTable(
                name: "USR_Trainings");

            migrationBuilder.DropTable(
                name: "USR_VolunteeringExperiences");

            migrationBuilder.DropTable(
                name: "NMS_Nominations");

            migrationBuilder.DropTable(
                name: "USR_Certifications");

            migrationBuilder.DropTable(
                name: "RST_Requests");

            migrationBuilder.DropTable(
                name: "USR_Projects");

            migrationBuilder.DropTable(
                name: "MST_EmploymentTypes");

            migrationBuilder.DropTable(
                name: "MST_Designations");

            migrationBuilder.DropTable(
                name: "MST_LocationTypes");

            migrationBuilder.DropTable(
                name: "MST_Proficiencies");

            migrationBuilder.DropTable(
                name: "MST_Skills");

            migrationBuilder.DropTable(
                name: "MST_TrainingTypes");

            migrationBuilder.DropTable(
                name: "MST_Trainings");

            migrationBuilder.DropTable(
                name: "MST_Causes");

            migrationBuilder.DropTable(
                name: "MST_InternalProgramCategories");

            migrationBuilder.DropTable(
                name: "MST_Certifications");

            migrationBuilder.DropTable(
                name: "MST_Companies");

            migrationBuilder.DropTable(
                name: "MST_Projects");

            migrationBuilder.DropTable(
                name: "MST_Departments");

            migrationBuilder.DropTable(
                name: "MST_InternalPrograms");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_AspNetUsers_EmployeeCode",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_BusinessUnitId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DesignationId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PronounId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "About",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BannerPhotoName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BusinessUnitId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "City",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DesignationId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EmergencyContactNo",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LinkedInUrl",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfilePhotoName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PronounId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SkypeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "State",
                table: "AspNetUsers");
        }
    }
}
