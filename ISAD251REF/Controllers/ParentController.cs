using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ISAD251REF.Controllers
{
    public class ParentController : Controller
    {
        public IActionResult Index()
        {
            if (TempData["IsValid"] != null) 
            {
                ViewBag.IsValid = "true";

            }
            return View();
        }
    }
}
