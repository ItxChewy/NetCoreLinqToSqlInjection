using System.Data;
using System.Numerics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using NetCoreLinqToSqlInjection.Models;

namespace NetCoreLinqToSqlInjection.Repositories
{

    #region PROCEDURE

    //CREATE PROCEDURE SP_DELETE_DOCTOR
    //(@IDDOCTOR INT)
    //AS
    //    DELETE FROM DOCTOR WHERE DOCTOR_NO = @IDDOCTOR
    //GO

    //    CREATE PROCEDURE SP_UPDATE_DOCTOR
    //(@IDHOSPITAL INT, @IDDOCTOR INT, @APELLIDO NVARCHAR(50)
    //,@ESPECIALIDAD NVARCHAR(50),@SALARIO INT)
    //AS
    //    UPDATE DOCTOR SET HOSPITAL_COD=@IDHOSPITAL,APELLIDO=@APELLIDO
    //	, ESPECIALIDAD=@ESPECIALIDAD, SALARIO=@SALARIO
    //    WHERE DOCTOR_NO=@IDDOCTOR
    //GO
    #endregion
    public class RepositoryDoctoresSQLServer : IRepositoryDoctores
    {
        private DataTable tableDoctores;
        private SqlConnection cn;
        private SqlCommand com;
        public RepositoryDoctoresSQLServer()
        {
            string connectionString = @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Trust Server Certificate=True";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
            this.tableDoctores = new DataTable();
            SqlDataAdapter ad = new SqlDataAdapter("select * from DOCTOR", connectionString);
            ad.Fill(this.tableDoctores);
        }

        public void DeleteDoctor(int idDoctor)
        {
            string sql = "SP_DELETE_DOCTOR";
            this.com.Parameters.AddWithValue("@IDDOCTOR", idDoctor);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }

        public Doctor DetallesDoctor(int idDoctor)
        {
            var consulta = from datos in this.tableDoctores.AsEnumerable()
                           where datos.Field<int>("DOCTOR_NO") == idDoctor
                           select datos;
            var row = consulta.First();
            Doctor doc = new Doctor();
            doc.IdHospital = row.Field<int>("HOSPITAL_COD");
            doc.IdDoctor = row.Field<int>("DOCTOR_NO");
            doc.Apellido = row.Field<string>("APELLIDO");
            doc.Especialidad = row.Field<string>("ESPECIALIDAD");
            doc.Salario = row.Field<int>("SALARIO");
            return doc;
        }

        public List<Doctor> DoctoresPorEspecialidad(string especialidad)
        {
            var consulta = from datos in this.tableDoctores.AsEnumerable()
                           where datos.Field<string>("ESPECIALIDAD") == especialidad
                           select datos;
            List<Doctor> doctores = new List<Doctor>();
            foreach(var row in consulta)
            {
                Doctor doc = new Doctor();
                doc.IdDoctor = row.Field<int>("DOCTOR_NO");
                doc.Apellido = row.Field<string>("APELLIDO");
                doc.Especialidad = row.Field<string>("ESPECIALIDAD");
                doc.Salario = row.Field<int>("SALARIO");
                doc.IdHospital = row.Field<int>("HOSPITAL_COD");
                doctores.Add(doc);
            }
            return doctores;
        }

        public List<Doctor> GetDoctores()
        {
            var consulta = from datos in this.tableDoctores.AsEnumerable()
                           select datos;
            List<Doctor> doctores = new List<Doctor>();
            foreach(var row in consulta)
            {
                Doctor doc = new Doctor();
                doc.IdDoctor = row.Field<int>("DOCTOR_NO");
                doc.Apellido = row.Field<string>("APELLIDO");
                doc.Especialidad = row.Field<string>("ESPECIALIDAD");
                doc.Salario = row.Field<int>("SALARIO");
                doc.IdHospital = row.Field<int>("HOSPITAL_COD");
                doctores.Add(doc);
            }
            return doctores;
        }

        public void InsertDoctor(int idDoctor, string apellido, string especialidad
            , int salario, int idHospital)
        {
            string sql = "insert into DOCTOR values(@idhospital,@iddoctor,@apellido,@especialidad,@salario)";
            this.com.Parameters.AddWithValue("@iddoctor", idDoctor);
            this.com.Parameters.AddWithValue("@apellido", apellido);
            this.com.Parameters.AddWithValue("@especialidad", especialidad);
            this.com.Parameters.AddWithValue("@salario", salario);
            this.com.Parameters.AddWithValue("@idhospital", idHospital);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();

        }

        public void UpdateDoctor(int idDoctor, string apellido, string especialidad, int salario, int idHospital)
        {
            string sql = "SP_UPDATE_DOCTOR";
            this.com.Parameters.AddWithValue("@idhospital", idHospital);
            this.com.Parameters.AddWithValue("@iddoctor", idDoctor);
            this.com.Parameters.AddWithValue("@apellido", apellido);
            this.com.Parameters.AddWithValue("@especialidad", especialidad);
            this.com.Parameters.AddWithValue("@salario", salario);
            
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();

        }
    }
}
