using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sube2.HelloMvc.Models;
using System.Linq;

namespace Sube2.HelloMvc.Controllers
{
    public class StudentController : Controller
    {
        private readonly OkulDbContext _context;

        public StudentController(OkulDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var lst = _context.Ogrenciler.Include(s => s.Dersler).ToList();
            return View(lst);
        }

        [HttpGet]
        public IActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddStudent(Ogrenci ogr)
        {
            if (ogr != null)
            {
                _context.Ogrenciler.Add(ogr);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult EditStudent(int id)
        {
            var ogr = _context.Ogrenciler.Find(id);
            return View(ogr);
        }

        [HttpPost]
        public IActionResult EditStudent(Ogrenci ogr)
        {
            if (ogr != null)
            {
                _context.Entry(ogr).State = EntityState.Modified;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteStudent(int id)
        {
            var ogr = _context.Ogrenciler.Find(id);
            if (ogr != null)
            {
                _context.Ogrenciler.Remove(ogr);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
