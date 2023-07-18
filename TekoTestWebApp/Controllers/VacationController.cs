using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using TekoTestWebApp.Interfaces;
using TekoTestWebApp.Models;
using TekoTestWebApp.ViewModels;

namespace TekoTestWebApp.Controllers
{
    public class VacationController : Controller
    {
        private readonly IVacationRepository _vacationRepository;
        private readonly IVacationService _vacationService;

        public VacationController(IVacationRepository vacationRepository, IVacationService vacationService)
        {
            _vacationRepository = vacationRepository;
            _vacationService = vacationService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Vacation> vacations = await _vacationRepository.GetAllAsync();
            return View(vacations);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Vacation vacation = await _vacationRepository.GetByIdAsync(id);
            if (vacation == null)
            {
                return View("Error");
            }

            var departmentIntersections = _vacationService.GetDepartmentIntersections(vacation);
            var womenIntersectons = _vacationService.GetWomenIntersections(vacation);
            var age50PlusIntersections = _vacationService.GetAge50PlusIntersections(vacation);
            var nonIntersectingVacations = _vacationService.GetNonIntersectingVacations(vacation);

            var detailVacationViewModel = new DetailVacationViewModel
            {
                Vacation = vacation,
                DepartmentIntersections = departmentIntersections,
                WomenIntersections = womenIntersectons,
                Age50PlusIntersections = age50PlusIntersections,
                NonIntersectingVacations = nonIntersectingVacations
            };

            return View(detailVacationViewModel);
        }

        public IActionResult Create() 
        {
            ViewBag.UserList = new SelectList(_vacationRepository.GetUsers(), "Id", "FullName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateVacationViewModel vacationViewModel)
        {
            if (ModelState.IsValid)
            {
                DateTime start = vacationViewModel.StartDateTime;
                DateTime end = vacationViewModel.EndDateTime;
                int length = ((TimeSpan)(end - start)).Days + 1;

                if (length > 14)
                {
                    ModelState.AddModelError("", "Length of vacation is more than 14 days");
                    return View("Error", vacationViewModel);
                }

                var vacation = new Vacation
                {
                    StartDateTime = vacationViewModel.StartDateTime,
                    EndDateTime = vacationViewModel.EndDateTime,
                    User = vacationViewModel.User
                };
                _vacationRepository.Add(vacation);
                return RedirectToAction("Index");
            }
            return View(vacationViewModel);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var vacation = await _vacationRepository.GetByIdAsync(id);
            if (vacation == null) return View("Error");
            var vacationViewModel = new EditVacationViewModel
            {
                StartDateTime = vacation.StartDateTime,
                EndDateTime = vacation.EndDateTime,
                User = vacation.User
            };
            return View(vacationViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditVacationViewModel vacationViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit vacaction");
                return View("Error", vacationViewModel);
            }

            var currentVacation = await _vacationRepository.GetByIdNoTracking(id);
            if (currentVacation != null)
            {
                DateTime start = vacationViewModel.StartDateTime;
                DateTime end = vacationViewModel.EndDateTime;
                int length = ((TimeSpan)(end - start)).Days + 1;

                if (length > 14)
                {
                    ModelState.AddModelError("", "Length of vacation is more than 14 days");
                    return View("Error", vacationViewModel);
                }

                var tempVacation = new Vacation
                {
                    Id = id,
                    StartDateTime = vacationViewModel.StartDateTime,
                    EndDateTime = vacationViewModel.EndDateTime,
                    User = vacationViewModel.User
                };
                _vacationRepository.Update(tempVacation);
                return RedirectToAction("Index");
            }
            return View(vacationViewModel);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteVacation(int id)
        {
            var vacationDetail = await _vacationRepository.GetByIdAsync(id);
            if (vacationDetail != null) return View(vacationDetail);
            _vacationRepository.Delete(vacationDetail);
            return RedirectToAction("Index");
        }
    }
}
