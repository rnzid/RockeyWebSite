using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rockys_DataAccess;
using Rockys_DataAccess.Repository.IRepository;
using Rockys_Models;
using Rockys_Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rockys.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class ApplicationTypeController : Controller
    {
        private readonly IApplicationTypeRepository _appTypeRepo;

        public ApplicationTypeController(IApplicationTypeRepository appTypeRepo)
        {
            _appTypeRepo = appTypeRepo;
        }

        public IActionResult Index()
        {
            IEnumerable<ApplicationType> objList = _appTypeRepo.GetAll();
            return View(objList);
        }

        //Get-Create
        public IActionResult Create()
        {

            return View();
        }

        //Post-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ApplicationType obj)
        {
            if (ModelState.IsValid)
            {
                _appTypeRepo.Add(obj);
                _appTypeRepo.Save();
                return RedirectToAction("Index");
            }
            return View(obj);

        }


        //Get-Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _appTypeRepo.Find(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //Post-Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ApplicationType obj)
        {
            if (ModelState.IsValid)
            {
                _appTypeRepo.Update(obj);
                _appTypeRepo.Save();
                return RedirectToAction("Index");
            }
            return View(obj);

        }


        //Get-Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _appTypeRepo.Find(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //Post-Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _appTypeRepo.Find(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }

            _appTypeRepo.Remove(obj);
            _appTypeRepo.Save();
            return RedirectToAction("Index");

        }
    }
}
