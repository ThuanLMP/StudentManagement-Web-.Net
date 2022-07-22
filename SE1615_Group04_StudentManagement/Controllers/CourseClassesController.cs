using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SE1615_Group04_StudentManagement.Models;

namespace SE1615_Group04_StudentManagement.Controllers
{
    public class CourseClassesController : Controller
    {
        private readonly StudentManagermentContext _context;

        public CourseClassesController(StudentManagermentContext context)
        {
            _context = context;
        }

        // GET: CourseClasses
        public async Task<IActionResult> Index(String id)
        {

            var studentManagermentContext = _context.CourseClasses.Include(c => c.Class).Include(c => c.Subject).Where(c => c.Classid == id).OrderByDescending(c=>c.Classid);
            if (id == null)
            {
                studentManagermentContext = _context.CourseClasses.Include(c => c.Class).Include(c => c.Subject).OrderByDescending(c=>c.Classid);
            }

            return View(await studentManagermentContext.ToListAsync());
        }

        // GET: CourseClasses/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseClass = await _context.CourseClasses
                .Include(c => c.Class)
                .Include(c => c.Subject)
                .FirstOrDefaultAsync(m => m.Subjectid == id);
            if (courseClass == null)
            {
                return NotFound();
            }

            return View(courseClass);
        }

        // GET: CourseClasses/Create
        public IActionResult Create(string id)
        {
            ViewData["Classid"] = new SelectList(_context.Classes.Where(c => c.Classid == id), "Classid", "Classid");
            var listCourse = _context.CourseClasses.Where(c => c.Classid == id).Select(c => c.Subjectid).ToList();
            var listCourse1 = _context.Subjects.Select(c => c.Subjectid).ToList();
            ViewData["Subjectid"] = new SelectList(listCourse1.Except(listCourse));
            return View();
        }

        // POST: CourseClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Subjectid,Classid")] CourseClass courseClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Classid"] = new SelectList(_context.Classes, "Classid", "Classid", courseClass.Classid);
            ViewData["Subjectid"] = new SelectList(_context.Subjects, "Subjectid", "Subjectid", courseClass.Subjectid);
            return View(courseClass);
        }

        // GET: CourseClasses/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseClass = await _context.CourseClasses.FindAsync(id);
            if (courseClass == null)
            {
                return NotFound();
            }
            ViewData["Classid"] = new SelectList(_context.Classes, "Classid", "Classid", courseClass.Classid);
            ViewData["Subjectid"] = new SelectList(_context.Subjects, "Subjectid", "Subjectid", courseClass.Subjectid);
            return View(courseClass);
        }

        // POST: CourseClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Subjectid,Classid")] CourseClass courseClass)
        {
            if (id != courseClass.Subjectid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseClassExists(courseClass.Subjectid))
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
            ViewData["Classid"] = new SelectList(_context.Classes, "Classid", "Classid", courseClass.Classid);
            ViewData["Subjectid"] = new SelectList(_context.Subjects, "Subjectid", "Subjectid", courseClass.Subjectid);
            return View(courseClass);
        }

        // GET: CourseClasses/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseClass = await _context.CourseClasses
                .Include(c => c.Class)
                .Include(c => c.Subject)
                .FirstOrDefaultAsync(m => m.Subjectid == id);
            if (courseClass == null)
            {
                return NotFound();
            }

            return View(courseClass);
        }

        // POST: CourseClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var courseClass = await _context.CourseClasses.FindAsync(id);
            _context.CourseClasses.Remove(courseClass);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseClassExists(string id)
        {
            return _context.CourseClasses.Any(e => e.Subjectid == id);
        }
    }
}