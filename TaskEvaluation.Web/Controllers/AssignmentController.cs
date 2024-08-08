using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskEvaluation.Core.Entities.Business;
using TaskEvaluation.Core.Entities.DTOs;
using TaskEvaluation.Core.Interfaces.IServices;
using TaskEvaluation.Infrastructure.Services;

namespace TaskEvaluation.Web.Controllers
{
    [Authorize]
    public class AssignmentController : Controller
    {
        private readonly IAssignmentService _assignmentService;
        private readonly ICourseService _courseService;
        public AssignmentController(IAssignmentService assignmentService, ICourseService courseService)
        {
            _assignmentService = assignmentService;
            _courseService = courseService; 
        }

        public async Task<IActionResult> Index()
        {
            var assignment = await _assignmentService.GetAssignmentsAsync(x => x.Solution,x=>x.Group,x=>x.Course);

            return View(assignment);
        }
        public async Task<IActionResult> Create(int courseId)
        {
            var courses = await _courseService.GetCoursesAsync();
            ViewBag.Courses =  new SelectList(courses, "Id", "Title",courseId);
            return View(new AssignmentDTO { CourseId = courseId });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AssignmentDTO assignmentDTO)
        {
            if (ModelState.IsValid)
            {
                await _assignmentService.CreateAsync(assignmentDTO);
                return RedirectToAction(nameof(Index));
            }
            var courses = await _courseService.GetCoursesAsync();
            ViewBag.Courses = new SelectList(courses, "Id", "Title", assignmentDTO.CourseId);
            return View(assignmentDTO);
        }
        public async Task<IActionResult> Details(int id)
        {
            var assignment = await _assignmentService.GetAssignmentAsync(id, x => x.Solution, x => x.Group, x => x.Course);
            if (assignment == null)
            {
                return NotFound();
            }
            return View(assignment);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var assignment = await _assignmentService.GetAssignmentAsync(id, x => x.Solution, x => x.Group, x => x.Course);
            if (assignment == null)
            {
                return NotFound();
            }
            var courses = await _courseService.GetCoursesAsync();
            ViewBag.Courses = new SelectList(courses, "Id", "Title", assignment.CourseId);
            return View(assignment);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AssignmentDTO assignmentDTO)
        {
            if (id != assignmentDTO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _assignmentService.UpdateAsync(assignmentDTO);
                return RedirectToAction(nameof(Index));
            }
            var courses = await _courseService.GetCoursesAsync();
            ViewBag.Courses = new SelectList(courses, "Id", "Title", assignmentDTO.CourseId);
            return View(assignmentDTO);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var assignment = await _assignmentService.GetAssignmentAsync(id, x => x.Solution, x => x.Group, x => x.Course);
            if (assignment == null)
            {
                return NotFound();
            }
            return View(assignment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _assignmentService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
