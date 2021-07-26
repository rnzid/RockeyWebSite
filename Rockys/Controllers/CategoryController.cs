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
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _CatRepo;

        public CategoryController(ICategoryRepository CatRepo)
        {
            _CatRepo = CatRepo;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objList = _CatRepo.GetAll();
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
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _CatRepo.Add(obj);
                _CatRepo.Save();
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
            var obj = _CatRepo.Find(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //Post-Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _CatRepo.Update(obj);
                _CatRepo.Save();
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
            var obj = _CatRepo.Find(id.GetValueOrDefault());
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
            var obj = _CatRepo.Find(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }

            _CatRepo.Remove(obj);
            _CatRepo.Save();
            return RedirectToAction("Index");

        }
    }
}
