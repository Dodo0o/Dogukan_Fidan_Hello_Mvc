using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sube2.HelloMvc.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Sube2.HelloMvc.Controllers
{
    public class DersController : Controller
    {
        private readonly OkulDbContext _context;

        public DersController(OkulDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var dersler = await _context.Dersler.ToListAsync();
            return View(dersler);
        }

       
        public IActionResult AddDers()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddDers(Ders ders)
        {
            if (ModelState.IsValid)
            {
                _context.Dersler.Add(ders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    System.Diagnostics.Debug.WriteLine($"ModelState Hatası: {error.ErrorMessage}");
                }
            }
            return View(ders);
        }

        public async Task<IActionResult> EditDers(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ders = await _context.Dersler.FindAsync(id);
            if (ders == null)
            {
                return NotFound();
            }
            return View(ders);
        }

        [HttpPost]
        public async Task<IActionResult> EditDers(int id, Ders ders)
        {
            if (id != ders.Dersid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DersExists(ders.Dersid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ders);
        }

  
        [HttpPost]
        public async Task<IActionResult> DeleteDers(int id)
        {
            var ders = await _context.Dersler.FindAsync(id);
            if (ders == null)
            {
                return NotFound();
            }

            _context.Dersler.Remove(ders);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult AddDersForOgrenci(int id)
        {
            var ogrenci = _context.Ogrenciler.Include(s => s.Dersler).FirstOrDefault(s => s.Ogrenciid == id);
            if (ogrenci == null)
            {
                return NotFound();
            }
            ViewBag.Dersler = new SelectList(_context.Dersler.ToList(), "Dersid", "Dersad");
            var viewModel = new OgrenciDersViewModel
            {
                Ogrenciid = ogrenci.Ogrenciid,
                Ad = ogrenci.Ad,
                Soyad = ogrenci.Soyad,
                Numara = ogrenci.Numara,
                Dersler = ogrenci.Dersler
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddDersForOgrenci(OgrenciDersViewModel model)
        {
            var ogrenci = _context.Ogrenciler.Include(s => s.Dersler).FirstOrDefault(s => s.Ogrenciid == model.Ogrenciid);
            var ders = await _context.Dersler.FirstOrDefaultAsync(d => d.Dersid == model.SelectedDersId);
            if (ogrenci != null && ders != null)
            {
                ogrenci.Dersler.Add(ders);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Student");
        }

        private bool DersExists(int id)
        {
            return _context.Dersler.Any(e => e.Dersid == id);
        }
        public async Task<IActionResult> StudentsForDers(int id)
        {
            var ders = await _context.Dersler
                .Include(d => d.Ogrenciler)
                .FirstOrDefaultAsync(d => d.Dersid == id);

            if (ders == null)
            {
                return NotFound();
            }

            var ogrenciler = ders.Ogrenciler.ToList();

            return View(ogrenciler);
        }
    }
}
