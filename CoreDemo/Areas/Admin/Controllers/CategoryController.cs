using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using System;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Concrete;

namespace CoreDemo.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class CategoryController : Controller
	{
		CategoryManager cm = new CategoryManager(new EfCategoryRepository());
		Context c = new Context();
		public IActionResult Index()
		{
			var values = cm.GetList();
			return View(values);
		}

		[HttpGet]
		public IActionResult AddCategory()
		{
			return View();
		}
		[HttpPost]
		public IActionResult AddCategory(Category p)
		{
			CategoryValidator cv = new CategoryValidator();
			ValidationResult results = cv.Validate(p);

			if (results.IsValid)
			{
				p.CategoryStatus = true;
				cm.TAdd(p);
				return RedirectToAction("Index", "Category");
			}
			else
			{
				foreach (var item in results.Errors)
				{
					ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
				}
			}
			return View();
		}

		[HttpGet]
		public IActionResult EditCategory(int id)
		{
			var category = cm.TGetByID(id);
			if (category != null)
			{
				return View(category);
			}
			else
			{
				return NotFound();
			}
		}

		[HttpPost]
		public IActionResult EditCategory(int id, Category category)
		{
			var categoryvalue = cm.TGetByID(id);
			if (categoryvalue != null)
			{
				categoryvalue.CategoryName = category.CategoryName;
				categoryvalue.CategoryDescription = category.CategoryDescription;
				categoryvalue.CategoryStatus = true;
				cm.TUpdate(categoryvalue);
				return RedirectToAction("Index");
			}
			else
			{
				return NotFound();
			}
		}

		public IActionResult CategoryDelete(int id)
		{
			var value = cm.TGetByID(id);
			cm.TDelete(value);
			return RedirectToAction("Index");
		}
	}
}
