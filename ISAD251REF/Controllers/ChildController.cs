using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ISAD251REF.Controllers
{
    public class ChildController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
