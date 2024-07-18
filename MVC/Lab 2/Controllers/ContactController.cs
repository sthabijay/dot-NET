using Lab_2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab_2.Controllers
{
	public class ContactController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
        public IActionResult Index(ContactModel model)
        {
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			ViewBag.SucessMessage = "Message received sucessfully";
			ModelState.Clear();

            return View();
        }
    }
}
