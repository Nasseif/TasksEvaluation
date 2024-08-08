using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskEvaluation.Core.Entities.Business;
using TaskEvaluation.Core.Entities.DTOs;
using TaskEvaluation.Core.Interfaces.IRepositories;
using TaskEvaluation.Core.Interfaces.IServices;
using TaskEvaluation.Core.Mapper;

namespace TaskEvaluation.Web.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {

        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService )
        {
            _courseService = courseService;         
        }
        
        public async Task<IActionResult> Index()
        {
            var course = await _courseService.GetCoursesAsync(x=>x.Groups,x=>x.Assignment);    
            return View(course);    
        }
        public IActionResult Create()
        {
            return View();  
        }
         [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseDTO course)
        {
            if (ModelState.IsValid)
            {
                await _courseService.CreateAsync(course);
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }
        public async Task<IActionResult> Details(int id)
        {
            var course = await _courseService.GetCourseAsync(id,x=>x.Groups, x => x.Assignment);   
            if (course == null)
            {
                return NotFound();  
            }
            return View(course);    
        }
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _courseService.GetCourseAsync(id,x=>x.Groups, x => x.Assignment);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  CourseDTO courseDTO)
        {
            if (id != courseDTO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _courseService.UpdateAsync(courseDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(courseDTO);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _courseService.GetCourseAsync(id,x=>x.Groups, x => x.Assignment);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _courseService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
