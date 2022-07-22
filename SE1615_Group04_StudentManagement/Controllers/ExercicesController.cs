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
    public class ExercicesController : Controller
    {
        private readonly StudentManagermentContext _context;

        public ExercicesController(StudentManagermentContext context)
        {
            _context = context;
        }

        // GET: Exercices
        public async Task<IActionResult> Index()
        {
            var studentManagermentContext = _context.Exercices.Include(e => e.Subject);
            return View(await studentManagermentContext.ToListAsync());
        }

        // GET: Exercices/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercice = await _context.Exercices
                .Include(e => e.Subject)
                .FirstOrDefaultAsync(m => m.Name == id);
            if (exercice == null)
            {
                return NotFound();
            }

            return View(exercice);
        }

        // GET: Exercices/Create
        public IActionResult Create()
        {
            ViewData["Subjectid"] = new SelectList(_context.Subjects, "Subjectid", "Subjectid");
            return View();
        }

        // POST: Exercices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Subjectid,Percentage")] Exercice exercice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exercice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Subjectid"] = new SelectList(_context.Subjects, "Subjectid", "Subjectid", exercice.Subjectid);
            return View(exercice);
        }

        // GET: Exercices/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercice = await _context.Exercices.FindAsync(id);
            if (exercice == null)
            {
                return NotFound();
            }
            ViewData["Subjectid"] = new SelectList(_context.Subjects, "Subjectid", "Subjectid", exercice.Subjectid);
            return View(exercice);
        }

        // POST: Exercices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Subjectid,Percentage")] Exercice exercice)
        {
            if (id != exercice.Name)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exercice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExerciceExists(exercice.Name))
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
            ViewData["Subjectid"] = new SelectList(_context.Subjects, "Subjectid", "Subjectid", exercice.Subjectid);
            return View(exercice);
        }

        // GET: Exercices/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercice = await _context.Exercices
                .Include(e => e.Subject)
                .FirstOrDefaultAsync(m => m.Name == id);
            if (exercice == null)
            {
                return NotFound();
            }

            return View(exercice);
        }

        // POST: Exercices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var exercice = await _context.Exercices.FindAsync(id);
            _context.Exercices.Remove(exercice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExerciceExists(string id)
        {
            return _context.Exercices.Any(e => e.Name == id);
        }
    }
}
