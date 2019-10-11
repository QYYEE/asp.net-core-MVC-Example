using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DD.Electricity.Cloud.AuthServer.Controllers
{
    public class IndustryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}