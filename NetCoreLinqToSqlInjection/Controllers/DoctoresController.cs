using Microsoft.AspNetCore.Mvc;
using NetCoreLinqToSqlInjection.Models;
using NetCoreLinqToSqlInjection.Repositories;

namespace NetCoreLinqToSqlInjection.Controllers
{
    public class DoctoresController : Controller
    {
        IRepositoryDoctores repo;

        public DoctoresController(IRepositoryDoctores repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            List<Doctor> doctores = this.repo.GetDoctores();
            return View(doctores);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Doctor doc)
        {
            this.repo.InsertDoctor(doc.IdDoctor, doc.Apellido, doc.Especialidad
                , doc.Salario, doc.IdHospital);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int iddoctor)
        {
            this.repo.DeleteDoctor(iddoctor);
            return RedirectToAction("Index");
        }

        public IActionResult Update(int iddoctor)
        {
            Doctor doc = this.repo.DetallesDoctor(iddoctor);
            return View(doc);
        }

        [HttpPost]
        public IActionResult Update(Doctor doc)
        {
            this.repo.UpdateDoctor(doc.IdDoctor, doc.Apellido, doc.Especialidad, doc.Salario, doc.IdHospital);
            return RedirectToAction("Index");
        }

        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(string especialidad)
        {
            List<Doctor> doctores = this.repo.DoctoresPorEspecialidad(especialidad);
            return View(doctores);
        }
    }
}
