using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CoreDemo.Controllers
{
	[Authorize(Roles = "Admin,Writer")]
	public class DashboardController : Controller
	{
		public IActionResult Index()
		{
			Context c = new Context();

			var username = User.Identity.Name;
			var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
			var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(x => x.WriterID).FirstOrDefault();

			ViewBag.v1 = c.Blogs.Count().ToString();
			ViewBag.v2 = c.Blogs.Count(x => x.WriterID == writerID).ToString();
			ViewBag.v3 = c.Categories.Count().ToString();
			return View();
		}
	}
}
