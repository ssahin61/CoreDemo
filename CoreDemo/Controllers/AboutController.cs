using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Controllers
{
	[AllowAnonymous]
	public class AboutController : Controller
	{
		AboutManaer abm = new AboutManaer(new EfAboutRepository());
		public IActionResult Index()
		{
			var values = abm.GetList();
			return View(values);
		}

		public PartialViewResult SocialMediaAbout()
		{
			return PartialView();
		}
	}
}
