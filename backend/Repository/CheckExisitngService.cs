using LinkwayAPI.Models;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Dynamic;

namespace LinkwayAPI.Repository
{
    public class CheckExisitngService : ICheckExisitngService
    {
        private readonly LinkwayDbContext _dbContextLinkway;

        public CheckExisitngService(LinkwayDbContext dbContextLinkway)
        {
            _dbContextLinkway = dbContextLinkway;
        }


        public async Task<bool> CheckExisitngAsync(string tblName, string identifier, string value)
        {
            try
            {


                string query = $"SELECT * FROM {tblName} WHERE {identifier} = '{value}'";

                var connection = _dbContextLinkway.Database.GetDbConnection();
                var statement = _dbContextLinkway.Database.GetDbConnection().CreateCommand();
                statement.CommandText = query;
                if (connection.State == ConnectionState.Closed)
                {
                    await connection.OpenAsync();
                }

                var result = new List<dynamic>();
                using (var reader = await statement.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var row = new ExpandoObject();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var name = reader.GetName(i);
                            var values = reader[i];
                            ((IDictionary<string, object>)row).Add(name, values);
                        }
                        result.Add(row);
                    }
                }

                await statement.Connection.CloseAsync();

                if (result.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }

}
