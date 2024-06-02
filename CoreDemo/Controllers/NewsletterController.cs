using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Controllers
{
	[AllowAnonymous]
	public class NewsletterController : Controller
	{
		NewsletterManager nm = new NewsletterManager(new EfNewsletterRepository());

		[HttpGet]
		public PartialViewResult SubscribeMail()
		{
			return PartialView();
		}

		[HttpPost]
		public IActionResult SubscribeMail(Newsletter newsletter)
		{
			newsletter.MailStatus = true;
			nm.AddNewsletter(newsletter);
			return RedirectToAction("Index", "Blog"); // bir şekilde yaptık.. ders 39
		}
	}
}
