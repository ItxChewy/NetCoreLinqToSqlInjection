using System;
using System.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Numerics;
using NetCoreLinqToSqlInjection.Models;
using Oracle.ManagedDataAccess.Client;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;

namespace NetCoreLinqToSqlInjection.Repositories
{
    #region PROCEDURES
    //CREATE OR REPLACE PROCEDURE SP_DELETE_DOCTOR
    //(P_IDDOCTOR DOCTOR.DOCTOR_NO%TYPE)
    //AS
    //BEGIN
    //  DELETE FROM DOCTOR WHERE DOCTOR_NO=P_IDDOCTOR;
    //  COMMIT;
    //END;

    //    CREATE OR REPLACE PROCEDURE SP_UPDATE_DOCTOR(
    //    p_IDHOSPITAL IN NUMBER,
    //    p_IDDOCTOR IN NUMBER,
    //    p_APELLIDO IN NVARCHAR2,
    //    p_ESPECIALIDAD IN NVARCHAR2,
    //    p_SALARIO IN NUMBER
    //) AS
    //BEGIN
    //    UPDATE DOCTOR
    //    SET HOSPITAL_COD = p_IDHOSPITAL,
    //        APELLIDO = p_APELLIDO,
    //        ESPECIALIDAD = p_ESPECIALIDAD,
    //        SALARIO = p_SALARIO
    //    WHERE DOCTOR_NO = p_IDDOCTOR;
    //    COMMIT;
    //END;
    #endregion
    public class RepositoryDoctoresOracle : IRepositoryDoctores
    {
        private DataTable tablaDoctores;
        private OracleConnection cn;
        private OracleCommand com;

        public RepositoryDoctoresOracle()
        {
            string connectionString = @"Data Source=LOCALHOST:1521/XE; Persist Security Info=True; User Id=SYSTEM; Password=oracle";
            this.tablaDoctores = new DataTable();
            this.cn = new OracleConnection(connectionString);
            this.com = new OracleCommand();
            this.com.Connection = this.cn;
            OracleDataAdapter ad =
                new OracleDataAdapter("select * from DOCTOR", connectionString);
            ad.Fill(this.tablaDoctores);
        }

        public void DeleteDoctor(int idDoctor)
        {
            string sql = "SP_DELETE_DOCTOR";
            OracleParameter pamIdDoctor = new OracleParameter(":P_IDDOCTOR", idDoctor);
            this.com.Parameters.Add(pamIdDoctor);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.com.Parameters.Clear();
        }

        public List<Doctor> GetDoctores()
        {
            var consulta = from datos in this.tablaDoctores.AsEnumerable()
                           select datos;
            List<Doctor> doctores = new List<Doctor>();
            foreach (var row in consulta)
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

        public List<Doctor> DoctoresPorEspecialidad(string especialidad)
        {
            var consulta = from datos in this.tablaDoctores.AsEnumerable()
                           where datos.Field<string>("ESPECIALIDAD") == especialidad
                           select datos;
            List<Doctor> doctores = new List<Doctor>();
            foreach (var row in consulta)
            {
                Doctor doc = new Doctor();
                doc.IdHospital = row.Field<int>("HOSPITAL_COD");
                doc.IdDoctor = row.Field<int>("DOCTOR_NO");
                doc.Apellido = row.Field<string>("APELLIDO");
                doc.Especialidad = row.Field<string>("ESPECIALIDAD");
                doc.Salario = row.Field<int>("SALARIO");
                
                doctores.Add(doc);
            }
            return doctores;
        }

        public Doctor DetallesDoctor(int idDoctor)
        {
            var consulta = from datos in this.tablaDoctores.AsEnumerable()
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

        public void InsertDoctor(int idDoctor, string apellido, string especialidad, int salario, int idHospital)
        {
            string sql = "insert into DOCTOR values(:idhospital,:iddoctor,:apellido,:especialidad,:salario)";

            OracleParameter pamIdHos = new OracleParameter(":idhospital", idHospital);
            this.com.Parameters.Add(pamIdHos);

            OracleParameter pamIdDoctor = new OracleParameter(":iddoctor", idDoctor);
            this.com.Parameters.Add(pamIdDoctor);

            OracleParameter pamApe = new OracleParameter(":apellido", apellido);
            this.com.Parameters.Add(pamApe);

            OracleParameter pamEsp = new OracleParameter(":especialidad", especialidad);
            this.com.Parameters.Add(pamEsp);

            OracleParameter pamSal = new OracleParameter(":salario", salario);
            this.com.Parameters.Add(pamSal);

            

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
            OracleParameter pamIdHos = new OracleParameter(":p_idhospital", idHospital);
            this.com.Parameters.Add(pamIdHos);

            OracleParameter pamIdDoctor = new OracleParameter(":p_iddoctor", idDoctor);
            this.com.Parameters.Add(pamIdDoctor);

            OracleParameter pamApe = new OracleParameter(":p_apellido", apellido);
            this.com.Parameters.Add(pamApe);

            OracleParameter pamEsp = new OracleParameter(":p_especialidad", especialidad);
            this.com.Parameters.Add(pamEsp);

            OracleParameter pamSal = new OracleParameter(":p_salario", salario);
            this.com.Parameters.Add(pamSal);


            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.com.Parameters.Clear();
        }
    }
}
