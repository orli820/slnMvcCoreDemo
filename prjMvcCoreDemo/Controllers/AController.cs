using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjMvcCoreDemo.Models;
using prjXamlDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace prjMvcCoreDemo.Controllers
{
    public class AController : Controller
    {
        public string demoSession()
        {
            TProduct x = new TProduct() { FName="XBOX",FPrice=12035,FCost=28};
            //string s ="{\"FName\":\""+x.FName +"\","+ x.FPrice.ToString() + "," + x.FCost.ToString();
            string s =JsonSerializer.Serialize(x);
            return s;
        }
        public IActionResult showCountBySession()
        {
            int count = 0;

            if (HttpContext.Session.Keys.Contains("KK"))
                count = (int)HttpContext.Session.GetInt32("KK");

            count++;
            HttpContext.Session.SetInt32("KK", count);

            ViewBag.COUNT = count;
            return View();
        }
        public string sayHello()
        {
            return "Hello ASP.NET MVC Core";
        }
        public string lotto()
        {
            return (new CLottoGen()).getNumber();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
