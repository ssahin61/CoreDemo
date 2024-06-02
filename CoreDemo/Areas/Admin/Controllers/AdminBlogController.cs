using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class AdminBlogController : Controller
	{
		BlogManager bm = new BlogManager(new EfBlogRepository());

		public IActionResult Index()
		{
			var values = bm.GetBlogListWithCategory();
			return View(values);
		}

	}
}
