using Microsoft.Data.SqlClient;

namespace SLAManager.Utils
{
    public static class DB
    {
        public static bool IsSqlServerAvailable(string connectionString)
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
                builder.TrustServerCertificate = true;
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                }
                return true;
            }
            catch (Exception e)
            {
               // Console.WriteLine("Eccezione :\n" + e.ToString());
                return false;
            }
        }
        public static bool ExecuteSqlCommand(string connectionString, string commandText)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine($"Comando eseguito con successo:\n{commandText}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Errore nell'esecuzione del comando:\n{commandText}\nErrore: {e.Message}");
                return false;
            }
            return true;
        }
        public static string[] GetSqlCommands(string sqlScript)
        {
            return sqlScript.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
