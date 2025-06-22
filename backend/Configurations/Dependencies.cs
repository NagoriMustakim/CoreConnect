using LinkwayAPI.Repository;
using LinkwayAPI.Repository.Interfaces;

namespace LinkwayAPI.Configurations
{
    public static class Dependencies
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRequestRepository, RequestRepository>();
            services.AddTransient<INominationRepository, NominationRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IBusinessUnitRepository, BusinessUnitRepository>();
            services.AddTransient<IPronounRepository, PronounRepository>();
            services.AddTransient<IEmploymentTypeRepository, EmploymentTypeRepository>();
            services.AddTransient<ILocationTypeRepository, LocationTypeRepository>();
            services.AddTransient<ITrainingTypeRepository, TrainingTypeRepository>();
            services.AddTransient<IFileRepository, FileRepository>();
            services.AddTransient<ICheckExisitngService, CheckExisitngService>();
            services.AddTransient<IInternalProgramsRepository, InternalProgramsRepository>();
            services.AddTransient<ISkillRepository, SkillRepository>();
            services.AddTransient<ITrainingRepository, TrainingRepository>();
            services.AddTransient<ICertificationRepository, CertificationRepository>();
            services.AddTransient<IProficiencyRepository, ProficiencyRepository>();
            services.AddTransient<IGiftProgramRepository, GiftProgramRepository>();
            services.AddTransient<ISearchRepository, SearchRepository>();
            services.AddTransient<IUSRSkillRepository, USRSkillRepository>();
            services.AddTransient<IUSRProjectRepository, USRProjectRepository>();
            services.AddTransient<IUSRLanguageRepository, USRLanguageRepository>();
            services.AddTransient<IUSRTrainingRepository, USRTrainingRepository>();
            services.AddTransient<IUSRAwardRepository, USRAwardRepository>();
            services.AddTransient<IUSRBasicDetailRepository, USRBasicDetailRepository>();
            services.AddTransient<IUSRCertificateRepository, USRCertificateRepository>();
            services.AddTransient<IUSREducationRepository, USREducationRepository>();
            services.AddTransient<IUSRExperienceRepository, USRExperienceRepository>();
            services.AddTransient<IUSRKraRepository, USRKraRepository>();
            services.AddTransient<IDashboardRepository, DashboardRepository>();
            services.AddTransient<IDesignationRepository, DesignationRepository>();
            services.AddTransient<ICompaniesRepository, ComponiesRepository>();
            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IInternalProgramCategoryRepository, InternalProgramCategoryRepository>();
            services.AddTransient<INotificationRepository, NotificationRepository>();
        }
    }
}
