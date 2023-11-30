using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using Web_API.Data;
using Microsoft.Extensions.Configuration;

namespace Web_API.Services
{
    public class StableService : IStableService
    {
        private readonly IConfiguration _configuration;

        public StableService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<Stable>> GetAllStablesAsync()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetAllStables", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        var stables = new List<Stable>();

                        while (await reader.ReadAsync())
                        {
                            Stable stable = new Stable
                            {
                                ID = reader.GetInt32(reader.GetOrdinal("id")),
                                Name = reader["name"].ToString()
                            };

                            stables.Add(stable);
                        }

                        return stables;
                    }
                }
            }
        }

        public async Task<List<EarTag>> GetEarTagsByStableIDAsync(int stableID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetEarTagsByStableID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@stableID", stableID);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        var earTags = new List<EarTag>();

                        while (await reader.ReadAsync())
                        {
                            EarTag earTag = new EarTag
                            {
                                ID = reader.GetInt32(reader.GetOrdinal("id")),
                                CentraleHusdyrbrugsRegisterNumber = reader.GetInt32(reader.GetOrdinal("centrale_husdyrbrugs_register_number")),
                                HerdNumber = reader.GetInt32(reader.GetOrdinal("herd_number")),
                                CountryCode = reader["country_code"].ToString()
                                //HerdNumber = reader["name"].ToString(),
                            };

                            earTags.Add(earTag);
                        }

                        return earTags;
                    }
                }
            }
        }

        public async Task<List<StableInfo>> GetAllStableInfosAsync()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetAllStables", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        var stableInfos = new List<StableInfo>();
                        while (await reader.ReadAsync())
                        {
                            int stableID = reader.GetInt32(reader.GetOrdinal("id"));
                            StableInfo stableInfo = new StableInfo
                            {
                                Stable = new Stable
                                {
                                    ID = stableID,
                                    Name = reader["name"].ToString()
                                },
                                HerdNumbers = await GetHerdNumbersByStableIDAsync(stableID)
                            };
                            stableInfos.Add(stableInfo);
                        };
                        return stableInfos;
                    }
                }
            }
        }

        public async Task<List<int>> GetHerdNumbersByStableIDAsync(int stableID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetHerdsByStableID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@stableID", stableID);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        var herdNumbers = new List<int>();

                        while (await reader.ReadAsync())
                        {
                            int heardNumber = reader.GetInt32(reader.GetOrdinal("herd_number"));

                            herdNumbers.Add(heardNumber);
                        }

                        return herdNumbers;
                    }
                }
            }
        }

        /*
        public async Task<List<EarTag>> GetAllEarTagsAsync()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (var command = new SqlCommand("GetAllEarTags", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        var earTags = new List<EarTag>();

                        while (await reader.ReadAsync())
                        {
                            var earTag = new EarTag
                            {
                                CountryCodeAlpha2 = reader.GetString(2),
                                Chr = reader.GetInt32(0),
                                HerdNumber = reader.GetInt32(1)
                            };

                            earTags.Add(earTag);
                        }

                        return earTags;
                    }
                }
            }
        }
        */

        /*
        public async Task<List<Pig>> GetAllPigsAsync()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (var command = new SqlCommand("GetAllPigs", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        var earTags = new List<Pig>();

                        while (await reader.ReadAsync())
                        {
                            var pig = new Pig
                            {
                                ID = reader.GetInt32(0),
                                EarTag = new EarTag
                                {
                                    Chr = reader.GetInt32(1),
                                    HerdNumber = reader.GetInt32(2),
                                    CountryCode = new CountryCode
                                    {

                                    }
                                };
                                CountryCodeAlpha2 = reader.GetString(2),
                                Chr = reader.GetInt32(0),
                                HerdNumber = reader.GetInt32(1)
                            };

                            earTags.Add(earTag);
                        }

                        return earTags;
                    }
                }
            }
        }
    */
    }
}
