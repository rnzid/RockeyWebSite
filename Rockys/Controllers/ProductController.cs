﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rockys.Data;
using Rockys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rockys.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> objList = _db.Product;
            foreach (var obj in objList)
            {
                obj.Category = _db.Category.FirstOrDefault(u => u.Id == obj.CategoryId);
            };
            return View(objList);
        }

        //Get-Create
        public IActionResult Upsert(int? id)
        {
            IEnumerable<SelectListItem> CategoryDropDown = _db.Category.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            ViewBag.CategoryDropDown = CategoryDropDown;

            Product product = new Product();
            if (id == null)
            {
                //For Create
                return View(product);
            }
            else
            {
                //update
                product = _db.Product.Find(id);
                if (product == null)
                {
                    return NotFound();
                }
                return View(product);
            }
        }

        //Post-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Product obj)
        {
            if (ModelState.IsValid)
            {
                _db.Product.Add(obj);
                _db.SaveChanges();
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
            var obj = _db.Product.Find(id);
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
            var obj = _db.Product.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Product.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
