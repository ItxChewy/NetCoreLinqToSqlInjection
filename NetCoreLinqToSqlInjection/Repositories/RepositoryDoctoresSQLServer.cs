using System.Data;
using Microsoft.Data.SqlClient;

namespace NetCoreLinqToSqlInjection.Repositories
{
    public class RepositoryDoctoresSQLServer
    {
        private DataTable tableDoctores;
        private SqlConnection cn;
        private SqlCommand com;
        public RepositoryDoctoresSQLServer()
        {
            string connectionString = @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Encrypt=True;Trust Server Certificate=True";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
            this.tableDoctores = new DataTable();
            SqlDataAdapter ad = new SqlDataAdapter("select * from DOCTOR", connectionString);
            ad.Fill(this.tableDoctores);
        }
    }
}
