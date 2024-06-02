using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using CoreDemo.Models;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
	[Authorize(Roles = "Admin,Writer")]
	public class WriterController : Controller
	{
		WriterManager wm = new WriterManager(new EfWriterRepository());
		UserManager um = new UserManager(new EfUserRepository());
		Context c = new Context();
		private readonly UserManager<AppUser> _userManager;

		public WriterController(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

		[Authorize]
		public IActionResult Index()
		{
			var usermail = User.Identity.Name;
			ViewBag.v1 = usermail;
			var writerName = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterName).FirstOrDefault();
			ViewBag.v2 = writerName;
			return View();
		}

		public IActionResult Test()
		{
			return View();
		}

		[HttpGet]
		public IActionResult ChangePassword()
		{
			var usermail = User.Identity.Name;
			Context c = new Context();
			var writerId = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
			var result = wm.TGetByID(writerId);
			return View(result);
		}

		[HttpPost]
		public IActionResult ChangePassword(Writer p)
		{
			return View();
		}


		[HttpGet]
		public async Task<IActionResult> WriterEditProfile()
		{
			var values = await _userManager.FindByNameAsync(User.Identity.Name);
			UserUpdateViewModel model = new UserUpdateViewModel();

			model.nameSurname = values.NameSurname;
			model.userName = values.UserName;
			model.imageURL = values.ImageURL;
			model.mail = values.Email;

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> WriterEditProfile(string PasswordAgain, UserUpdateViewModel model)
		{
			var values = await _userManager.FindByNameAsync(User.Identity.Name);

			values.NameSurname = model.nameSurname;
			values.UserName = model.userName;
			values.ImageURL = model.imageURL;
			values.Email = model.mail;

			if (model.password != null)
			{
				values.PasswordHash = _userManager.PasswordHasher.HashPassword(values, model.password);

			}
			await _userManager.UpdateAsync(values);

			return RedirectToAction("WriterEditProfile", "Writer");
		}

		[HttpGet]
		public IActionResult WriterAdd()
		{
			return View();
		}

		[HttpPost]
		public IActionResult WriterAdd(AddProfileImage p)
		{
			Writer w = new Writer();
			if (p.WriterImage != null)
			{
				var extension = Path.GetExtension(p.WriterImage.FileName);
				var newimage = Guid.NewGuid() + extension;
				var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/WriterImageFiles", newimage);
				var stream = new FileStream(location, FileMode.Create);
				p.WriterImage.CopyTo(stream);
				w.WriterImage = newimage;
			}
			w.WriterMail = p.WriterMail;
			w.WriterName = p.WriterName;
			w.WriterPassword = p.WriterPassword;
			w.WriterStatus = p.WriterStatus;
			w.WriterAbout = p.WriterAbout;
			wm.TAdd(w);
			return RedirectToAction("Index", "Dashboard");
		}
	}
}