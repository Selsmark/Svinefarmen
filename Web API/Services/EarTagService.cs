using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Web_API.Services
{
    public class EarTagService : IEarTagService
    {
        private readonly IConfiguration _configuration;

        public EarTagService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int InsertEarTag(string countryCode, int centraleHusdyrbrugsRegisterNumber, int herdNumber)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("InsertEarTag", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@countryCode", countryCode);
                    command.Parameters.AddWithValue("@centraleHusdyrbrugsRegisterNumber", centraleHusdyrbrugsRegisterNumber);
                    command.Parameters.AddWithValue("@herdNumber", herdNumber);

                    var ID = Convert.ToInt32(command.ExecuteScalar());

                    return ID;
                }
            }
        }
    }
}

