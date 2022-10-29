using Microsoft.AspNetCore.Mvc;
using prjMvcCoreDemo.Models;
using prjMvcCoreDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prjMvcCoreDemo.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult List(CKeywordViewModel model)
        {
            
            dbDemoContext db = new dbDemoContext();
            IEnumerable<TCustomer> datas = null;
            if (string.IsNullOrEmpty(model.txtKeyword))
                datas = from p in db.TCustomers
                        select p;
            else
                datas = db.TCustomers.Where(p => p.FName.Contains(model.txtKeyword) ||
                p.FAddress.Contains(model.txtKeyword) ||
                p.FPhone.Contains(model.txtKeyword) ||
                p.FEmail.Contains(model.txtKeyword) );

            return View(datas);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TCustomer p)
        {
            dbDemoContext db = new dbDemoContext();
            db.TCustomers.Add(p);
            db.SaveChanges();
            return RedirectToAction("List");
        }
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                dbDemoContext db = new dbDemoContext();
                TCustomer c = db.TCustomers.FirstOrDefault(t=>t.FId==id);
                if (c != null)
                {
                    db.TCustomers.Remove(c);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("List");
        }
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                dbDemoContext db = new dbDemoContext();
                TCustomer c = db.TCustomers.FirstOrDefault(t => t.FId == id);
                if (c != null)
                    return View(c);
            }
            return RedirectToAction("List");
        }
        [HttpPost]
        public ActionResult Edit(TCustomer inCust)
        {
            dbDemoContext db = new dbDemoContext();
            TCustomer c = db.TCustomers.FirstOrDefault(t => t.FId == inCust.FId);
            if (c != null)
            {
                c.FName = inCust.FName;
                c.FPhone = inCust.FPhone;
                c.FEmail = inCust.FEmail;
                c.FAddress = inCust.FAddress;
                c.FPassword = inCust.FPassword;
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

    }
}
