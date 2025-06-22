namespace LinkwayAPI.Repository.Interfaces
{
    public interface ICheckExisitngService
    {
        Task<bool> CheckExisitngAsync(string tblName, string identifier, string value);
    }
}
