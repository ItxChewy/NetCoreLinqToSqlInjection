using System.Data;
using NetCoreLinqToSqlInjection.Models;

namespace NetCoreLinqToSqlInjection.Repositories
{
    public interface IRepositoryDoctores
    {
        List<Doctor> GetDoctores();

        void InsertDoctor(int idDoctor, string apellido, string especialidad
           , int salario, int idHospital);

        void DeleteDoctor(int idDoctor);

        Doctor DetallesDoctor(int idDoctor);
        void UpdateDoctor(int idDoctor, string apellido, string especialidad
           , int salario, int idHospital);
        List<Doctor> DoctoresPorEspecialidad(string especialidad);
    }
}
