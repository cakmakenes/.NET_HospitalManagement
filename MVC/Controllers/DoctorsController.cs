using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers.Bases;
using BLL.Services;
using BLL.Models;
using BLL.DAL;
using BLL.Services.Bases;

// Generated from Custom Template.

namespace MVC.Controllers
{
    public class DoctorsController : MvcController
    {
        // Service injections:
        private readonly IService<Doctor,DoctorModel> _doctorService;
        private readonly IService<Branch,BranchModel> _branchService;

        /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
        //private readonly IManyToManyRecordService _ManyToManyRecordService;

        public DoctorsController(
            IService<Doctor, DoctorModel> doctorService
            , IService<Branch, BranchModel> branchService

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //, IManyToManyRecordService ManyToManyRecordService
        )
        {
            _doctorService = doctorService;
            _branchService = branchService;

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //_ManyToManyRecordService = ManyToManyRecordService;
        }

        // GET: Doctors
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _doctorService.Query().ToList();
            return View(list);
        }

        // GET: Doctors/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _doctorService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            ViewData["BranchId"] = new SelectList(_branchService.Query().ToList(), "Record.Id", "Name");
            
            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //ViewBag.ManyToManyRecordIds = new MultiSelectList(_ManyToManyRecordService.Query().ToList(), "Record.Id", "Name");
        }

        // GET: Doctors/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Doctors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DoctorModel doctor)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _doctorService.Create(doctor.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = doctor.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(doctor);
        }

        // GET: Doctors/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _doctorService.Query().SingleOrDefault(q => q.Record.Id == id);
            SetViewData();
            return View(item);
        }

        // POST: Doctors/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DoctorModel doctor)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _doctorService.Update(doctor.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = doctor.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(doctor);
        }

        // GET: Doctors/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _doctorService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        // POST: Doctors/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _doctorService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
