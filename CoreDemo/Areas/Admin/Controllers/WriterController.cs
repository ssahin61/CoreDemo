using CoreDemo.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace CoreDemo.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class WriterController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult WriterList()
		{
			var jsonWriters = JsonConvert.SerializeObject(writers);
			return Json(jsonWriters);
		}

		public IActionResult GetWriterByID(int writerid)
		{
			var findWriter = writers.FirstOrDefault(x => x.ID == writerid);
			var JsonWriters = JsonConvert.SerializeObject(findWriter);
			return Json(JsonWriters);
		}

		[HttpPost]
		public IActionResult AddWriter(WriterClass w)
		{
			writers.Add(w);
			var JsonWriters = JsonConvert.SerializeObject(w);
			return Json(JsonWriters);
		}

		public IActionResult DeleteWriter(int id)
		{
			var writer = writers.FirstOrDefault(x => x.ID == id);
			writers.Remove(writer);
			return Json(writer);
		}

		public IActionResult UpdateWriter(WriterClass w)
		{
			var writer = writers.FirstOrDefault(x => x.ID == w.ID);
			writer.Name = w.Name;
			var jsonWriter = JsonConvert.SerializeObject(w);
			return Json(writer);
		}

		public static List<WriterClass> writers = new List<WriterClass>
		{
			new WriterClass
			{
				ID = 1,
				Name = "Samet"
			},
			new WriterClass
			{
				ID = 2,
				Name = "Şevval"
			},
			new WriterClass
			{
				ID = 3,
				Name = "Nuran"
			},
			new WriterClass
			{
				ID = 4,
				Name = "Mete Serkan"
			}
		};
	}
}
