using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CoreDemo.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class MessageController : Controller
	{
		Message2Manager mm = new Message2Manager(new EfMessage2Repository());
		Context c = new Context();
		public IActionResult Inbox()
		{
			var username = User.Identity.Name;
			var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
			var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();

			var values = mm.GetInboxListByWriter(writerID);
			return View(values);
		}

		public IActionResult SendBox()
		{
			var username = User.Identity.Name;
			var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
			var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
			var values = mm.GetSendboxListByWriter(writerID);
			return View(values);
		}

		[HttpGet]
		public IActionResult SendMessage()
		{
			return View();
		}
		[HttpPost]
		public IActionResult SendMessage(Message2 p)
		{
			var username = User.Identity.Name;
			var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
			var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();

			p.SenderID = writerID;
			p.ReceiverID = 2;
			p.MessageStatus = true;
			p.MessageDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
			mm.TAdd(p);
			return RedirectToAction("SendBox");
		}
	}
}
