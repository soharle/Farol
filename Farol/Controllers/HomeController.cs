using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Farol.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Farol.Utils;
using System.Xml.Linq;

namespace Farol.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");

            var path = Path.Combine(Directory.GetCurrentDirectory(),
                                    "wwwroot",
                                    file.FileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            XMIReaderStarUML reader = new XMIReaderStarUML();
            XElement doc = XElement.Load(file.OpenReadStream());

            reader.LoadXmi(doc);
            OrderList orderList = new OrderList(reader.GetModel());

            ViewData["OrderList"] = orderList.Order;

            return View("Index");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
