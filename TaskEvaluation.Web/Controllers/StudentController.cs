using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskEvaluation.Core.Entities.DTOs;
using TaskEvaluation.Core.Interfaces.IRepositories;
using TaskEvaluation.Core.Interfaces.IServices;

namespace TaskEvaluation.Web.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
       
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
          
            _studentService = studentService;
        }

        public async Task<IActionResult> Index()
        {
            var students = await _studentService.GetStudentsAsync(x=>x.Group,x=>x.Solutions);
            return View(students);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentDTO student)
        {
            if (ModelState.IsValid)
            {
                await _studentService.CreateAsync(student);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        public async Task<IActionResult> Details(int id)
        {
            var student = await _studentService.GetStudentAsync(id, x => x.Group, x => x.Solutions);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var student = await _studentService.GetStudentAsync(id, x => x.Group, x => x.Solutions);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudentDTO studentDTO)
        {
            if (id != studentDTO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _studentService.UpdateAsync(studentDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(studentDTO);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var student = await _studentService.GetStudentAsync(id, x => x.Group, x => x.Solutions);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _studentService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
