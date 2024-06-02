using BusinessLayer.Concrete;
using CoreDemo.Models;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class RoleController : Controller
	{
		private readonly RoleManager<AppRole> _roleManager;
		private readonly UserManager<AppUser> _usermanager;

		public RoleController(RoleManager<AppRole> roleManager, UserManager<AppUser> usermanager)
		{
			_roleManager = roleManager;
			_usermanager = usermanager;
		}

		public IActionResult Index()
		{
			var values = _roleManager.Roles.ToList();
			return View(values);
		}

		[HttpGet]
		public IActionResult AddRole()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> AddRole(RoleViewModel model)
		{
			if (ModelState.IsValid)
			{
				AppRole role = new AppRole
				{
					Name = model.name
				};

				var result = await _roleManager.CreateAsync(role);
				if (result.Succeeded)
				{
					return RedirectToAction("Index");
				}
				else
				{
					foreach (var item in result.Errors)
					{
						ModelState.AddModelError("", item.Description);
					}
				}
			}
			return View(model);
		}

		[HttpGet]
		public IActionResult UpdateRole(int id)
		{
			var values = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
			RoleUpdateViewModel model = new RoleUpdateViewModel
			{
				ID = values.Id,
				name = values.Name
			};
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> UpdateRole(RoleUpdateViewModel model)
		{
			var values = _roleManager.Roles.Where(x => x.Id == model.ID).FirstOrDefault();

			values.Name = model.name;
			var result = await _roleManager.UpdateAsync(values);
			if (result.Succeeded)
			{
				return RedirectToAction("Index");
			}
			return View(model);
		}

		public async Task<IActionResult> DeleteRole(int id)
		{
			var values = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
			var result = await _roleManager.DeleteAsync(values);
			if (result.Succeeded)
			{
				return RedirectToAction("Index");
			}
			return View();
		}

		public IActionResult UserRoleList()
		{
			var values = _usermanager.Users.ToList();
			return View(values);
		}

		[HttpGet]
		public async Task<IActionResult> AssignRole(int id)
		{
			var user = _usermanager.Users.FirstOrDefault(x => x.Id == id);
			var roles = _roleManager.Roles.ToList();

			TempData["UserID"] = user.Id;

			var userRoles = await _usermanager.GetRolesAsync(user);

			List<RoleAssignViewModel> model = new List<RoleAssignViewModel>();
			foreach (var item in roles)
			{
				RoleAssignViewModel m = new RoleAssignViewModel();
				m.RoleID = item.Id;
				m.Name = item.Name;
				m.Exists = userRoles.Contains(item.Name);
				model.Add(m);
			}
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> AssignRole(List<RoleAssignViewModel> model)
		{
			var userid = (int)TempData["UserID"];
			var user = _usermanager.Users.FirstOrDefault(x => x.Id == userid);
			foreach (var item in model)
			{
				if (item.Exists)
				{
					await _usermanager.AddToRoleAsync(user, item.Name);
				}
				else
				{
					await _usermanager.RemoveFromRoleAsync(user, item.Name);
				}
			}
			return RedirectToAction("UserRoleList");
		}
	}
}
