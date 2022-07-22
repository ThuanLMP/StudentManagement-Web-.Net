using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SE1615_Group04_StudentManagement.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SE1615_Group04_StudentManagement.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentManagermentContext _context;

        public StudentsController(StudentManagermentContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index(IFormCollection values)
        {

            string sid = values["sid"];
            
            var studentManagermentContext = sid != null ? _context.Students
                .Include(s => s.Class)
                .Include(s => s.UsernameNavigation)
                .Where(s => s.Studentid == sid) : _context.Students
                .Include(s => s.Class)
                .Include(s => s.UsernameNavigation).OrderByDescending(s=>s.Classid);
            
            return View(await studentManagermentContext.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Class)
                .Include(s => s.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.Studentid == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            /*
            var listCourse = _context.CourseClasses.Where(c => c.Classid == id).Select(c => c.Subjectid).ToList();
            var listCourse1 = _context.Subjects.Select(c => c.Subjectid).ToList();
            ViewData["Subjectid"] = new SelectList(listCourse1.Except(listCourse));
            */
            var listAccount = _context.Accounts.Select(a => a.Username).ToList();
            var listAccount1 = _context.Students.Select(s => s.Username).ToList();
            listAccount.Remove("admin");
            ViewData["Username"] = new SelectList(listAccount.Except(listAccount1));
            ViewData["Classid"] = new SelectList(_context.Classes, "Classid", "Classid");
            
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,Studentid,Classid,Name,Gmail")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Classid"] = new SelectList(_context.Classes, "Classid", "Classid", student.Classid);
            ViewData["Username"] = new SelectList(_context.Accounts, "Username", "Username", student.Username);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            var listAccount = _context.Accounts.Select(a => a.Username).ToList();
            var listAccount1 = _context.Students.Select(s => s.Username).ToList();
            listAccount.Remove("admin");
            ViewData["Username"] = new SelectList(listAccount.Except(listAccount1));
            ViewData["Classid"] = new SelectList(_context.Classes, "Classid", "Classid", student.Classid);
            
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Username,Studentid,Classid,Name,Gmail")] Student student)
        {
            if (id != student.Studentid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Studentid))
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
            ViewData["Classid"] = new SelectList(_context.Classes, "Classid", "Classid", student.Classid);
            ViewData["Username"] = new SelectList(_context.Accounts, "Username", "Username", student.Username);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Class)
                .Include(s => s.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.Studentid == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(string id)
        {
            return _context.Students.Any(e => e.Studentid == id);
        }
    }
}
