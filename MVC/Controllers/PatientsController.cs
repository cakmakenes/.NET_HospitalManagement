﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers.Bases;
using BLL.Services;
using BLL.Models;
using BLL.DAL;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Authorization;

// Generated from Custom Template.

namespace MVC.Controllers
{
    [Authorize]
    public class PatientsController : MvcController
    {
        // Service injections:
        private readonly IService<Patient, PatientModel> _patientService;

        /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entity name in the controller and views. */
        private readonly IService<Doctor,DoctorModel> _doctorService;

        public PatientsController(
            IService<Patient, PatientModel> patientService

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            , IService<Doctor, DoctorModel> doctorService

        )
        {
            _patientService = patientService;

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            _doctorService = doctorService;
        }

        // GET: Patients
        [AllowAnonymous]
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _patientService.Query().ToList();
            return View(list);
        }

        // GET: Patients/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _patientService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            ViewBag.DoctorIds = new MultiSelectList(_doctorService.Query().ToList(), "Record.Id", "NameAndSurname");
        }

        // GET: Patients/Create
        [Authorize(Roles ="Admin")]
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Patients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public IActionResult Create(PatientModel patient)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _patientService.Create(patient.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = patient.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(patient);
        }

        // GET: Patients/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _patientService.Query().SingleOrDefault(q => q.Record.Id == id);
            SetViewData();
            return View(item);
        }

        // POST: Patients/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(PatientModel patient)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _patientService.Update(patient.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = patient.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(patient);
        }

        // GET: Patients/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _patientService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        [Authorize(Roles = "Admin")]
        // POST: Patients/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _patientService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
