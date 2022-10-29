using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjMvcCoreDemo.Models;
using prjMvcCoreDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace prjMvcCoreDemo.Controllers
{
    public class CommonController : Controller
    {
        public IActionResult Home()
        {
            if (!HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
                return RedirectToAction("Login");
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(CLoginViewModel vModel)
        {
            TCustomer cust = (new dbDemoContext()).TCustomers.FirstOrDefault(c => c.FEmail.Equals
            (vModel.txtAccount));
            if (cust != null)
            {
                if (cust.FPassword.Equals(vModel.txtPassword))
                {
                    string jsonUser = JsonSerializer.Serialize(cust);
                    HttpContext.Session.SetString(
                        CDictionary.SK_LOGINED_USER, jsonUser);

                    return RedirectToAction("Home");
                }
            }
            return View();
        }
    }
}
