using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using MVCText.Models;
using System.IO;

namespace MVCText.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IWebHostEnvironment _env;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public IActionResult Index()
        {
            var path = _env.WebRootPath;
            var fileNames = Directory.GetFiles($"{path}\\textFiles");
            for(int i = 0; i < fileNames.Length; i++)
                fileNames[i] = Path.GetFileNameWithoutExtension(fileNames[i]);
            StringContainer p = new StringContainer() {
                stuff = fileNames
            };
            return View(p);
        }

        public IActionResult Content(string id)
        {
            var path = _env.WebRootPath;
            string data = System.IO.File.ReadAllText($"{path}\\textFiles\\{id}.txt");
            ViewBag.text = data; 
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
