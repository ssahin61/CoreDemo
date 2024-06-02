using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Xml.Linq;

namespace CoreDemo.Areas.Admin.ViewComponents.Statistics
{
	public class Statistic4 : ViewComponent
	{
		Context c = new Context();
		public IViewComponentResult Invoke()
		{
			ViewBag.v1 = c.Admins.Where(x => x.AdminID == 1).Select(x => x.Name).FirstOrDefault();
			ViewBag.v2 = c.Admins.Where(x => x.AdminID == 1).Select(x => x.ImageURL).FirstOrDefault();
			ViewBag.v3 = c.Admins.Where(x => x.AdminID == 1).Select(x => x.ShortDescription).FirstOrDefault();

			return View();
		}
	}
}
