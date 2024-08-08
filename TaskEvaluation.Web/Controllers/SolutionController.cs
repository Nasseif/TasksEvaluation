using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskEvaluation.Core.Entities.DTOs;
using TaskEvaluation.Core.Interfaces.IServices;
using TaskEvaluation.Infrastructure.Services;

namespace TaskEvaluation.Web.Controllers
{
    [Authorize]
    public class SolutionController : Controller
    {
        private readonly ISolutionService _solutionService;

        public SolutionController(ISolutionService solutionService)
        {
            _solutionService = solutionService;
        }

        public async Task<IActionResult> Index()
        {
            var solution = await _solutionService.GetSolutionsAsync(x=>x.EvaluationGrade,x=>x.Assignment,x=>x.Student);

            return View(solution);
        }
        public IActionResult Create(int assignmentId)
        {
            ViewBag.AssignmentId = assignmentId;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SolutionDTO solutionDTO)
        {
            if (ModelState.IsValid)
            {
                await _solutionService.CreateAsync(solutionDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(solutionDTO);

        }

        public async Task<IActionResult> Details(int id)
        {
            var solution = await _solutionService.GetSolutionAsync(id, x => x.EvaluationGrade, x => x.Assignment, x => x.Student);
            if (solution == null)
            {
                return NotFound();
            }
            return View(solution);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _solutionService.GetSolutionAsync(id, x => x.EvaluationGrade, x => x.Assignment, x => x.Student);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SolutionDTO solutionDTO)
        {
            if (id != solutionDTO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _solutionService.UpdateAsync(solutionDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(solutionDTO);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _solutionService.GetSolutionAsync(id, x => x.EvaluationGrade, x => x.Assignment, x => x.Student);
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
            await _solutionService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }



    }
}
