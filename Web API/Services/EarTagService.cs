using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using Web_API.Data;

namespace Web_API.Services
{
    public class EarTagService : IEarTagService
    {
        private readonly IConfiguration _configuration;

        public EarTagService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<EarTag> AddEarTagAsync(EarTagInsertModel earTag)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("InsertEarTag", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@countryCode", earTag.CountryCode);
                    command.Parameters.AddWithValue("@centraleHusdyrbrugsRegisterNumber", earTag.CentraleHusdyrbrugsRegisterNumber);
                    command.Parameters.AddWithValue("@herdNumber", earTag.HerdNumber);

                    var ID = await command.ExecuteScalarAsync();

                    EarTag tempEarTag = new EarTag
                    {
                        ID = Convert.ToInt32(ID),
                        CountryCode = earTag.CountryCode,
                        CentraleHusdyrbrugsRegisterNumber = earTag.CentraleHusdyrbrugsRegisterNumber,
                        HerdNumber = earTag.HerdNumber
                    };

                    return tempEarTag;
                }
            }
        }

        public async Task<EarTag> GetEarTagByIDAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetEarTagByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@earTagID", id);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        EarTag earTag;

                        if (await reader.ReadAsync())
                        {
                            earTag = new EarTag
                            {
                                ID = reader.GetInt32(reader.GetOrdinal("id")),
                                CentraleHusdyrbrugsRegisterNumber = reader.GetInt32(reader.GetOrdinal("centrale_husdyrbrugs_register_number")),
                                HerdNumber = reader.GetInt32(reader.GetOrdinal("herd_number")),
                                CountryCode = reader["country_code"].ToString()
                            };
                        }
                        else
                        {
                            earTag = new EarTag
                            {
                                ID = 0,
                                CentraleHusdyrbrugsRegisterNumber = 0,
                                HerdNumber = 0,
                                CountryCode = ""
                            };
                        }

                        return earTag;
                    }
                }
            }
        }
    }
}
