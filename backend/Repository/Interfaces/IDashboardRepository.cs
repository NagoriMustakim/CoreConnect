namespace LinkwayAPI.Repository.Interfaces
{
    public interface IDashboardRepository
    {
        Task<object> GetAdminCountAsync();
    }
}