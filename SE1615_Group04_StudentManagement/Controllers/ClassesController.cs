using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SE1615_Group04_StudentManagement.Models;

namespace SE1615_Group04_StudentManagement.Controllers
{
    public class ClassesController : Controller
    {
        private readonly StudentManagermentContext _context;

        public ClassesController(StudentManagermentContext context)
        {
            _context = context;
        }

        // GET: Classes
        public async Task<IActionResult> Index()
        {
            
            string name = HttpContext.Session.GetString("UserName");

            var myClass = _context.Classes.Where(c => c.Classid == _context.Students.Where(s => s.Username == name).FirstOrDefault().Classid).OrderByDescending(s => s.Classid);
            if (name == "admin")
            {
                myClass = _context.Classes.OrderByDescending(c=>c.Classid);
            }

            return View(await myClass.ToListAsync());
            
        }
        

        // GET: Classes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["Students"] = await _context.Students.Where(s => s.Classid == id).ToListAsync();
            List<Student> ls = await _context.Students.Where(s => s.Classid == id).ToListAsync();
            ViewData["size"] = ls.Count();
            List<CourseClass> listCourse = await _context.CourseClasses.Where(c => c.Classid == id).ToListAsync();
            ViewData["listCourse"] = listCourse;
            ViewData["listSize"] = listCourse.Count();

            var @class = await _context.Classes
                .FirstOrDefaultAsync(m => m.Classid == id);
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // GET: Classes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Classes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Classid,Name,Startdate,Enddate")] Class @class)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@class);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@class);
        }

        // GET: Classes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @class = await _context.Classes.FindAsync(id);
            if (@class == null)
            {
                return NotFound();
            }
            return View(@class);
        }

        // POST: Classes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Classid,Name,Startdate,Enddate")] Class @class)
        {
            if (id != @class.Classid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@class);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassExists(@class.Classid))
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
            return View(@class);
        }

        // GET: Classes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @class = await _context.Classes
                .FirstOrDefaultAsync(m => m.Classid == id);
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var @class = await _context.Classes.FindAsync(id);
            _context.Classes.Remove(@class);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassExists(string id)
        {
            return _context.Classes.Any(e => e.Classid == id);
        }

    }
}
