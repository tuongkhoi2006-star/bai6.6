using System.Data.SqlClient;

namespace LibraryManagement.DAL
{
    public class DBConnection
    {
        // Chuỗi kết nối mặc định tới LocalDB của Visual Studio
        private static readonly string connStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LibraryDB;Integrated Security=True";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connStr);
        }
    }
}