using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using prjMvcCoreDemo.Models;
using prjMvcCoreDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace prjMvcCoreDemo.Controllers
{
    public class ProductController : Controller
    {
        private IWebHostEnvironment _enviro;
        public ProductController(IWebHostEnvironment p)
        {
            _enviro = p;
        }
        public IActionResult List(CKeywordViewModel model)
        {

            dbDemoContext db = new dbDemoContext();
            IEnumerable<TProduct> datas = null;
            if (string.IsNullOrEmpty(model.txtKeyword))
                datas = from p in db.TProducts
                        select p;
            else
                datas = db.TProducts.Where(p => p.FName.Contains(model.txtKeyword));

            return View(datas);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TProduct p)
        {
            dbDemoContext db = new dbDemoContext();
            db.TProducts.Add(p);
            db.SaveChanges();
            return RedirectToAction("List");
        }
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                dbDemoContext db = new dbDemoContext();
                TProduct c = db.TProducts.FirstOrDefault(t => t.FId == id);
                if (c != null)
                {
                    db.TProducts.Remove(c);
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
                TProduct c = db.TProducts.FirstOrDefault(t => t.FId == id);
                if (c != null)
                    return View(c);
            }
            return RedirectToAction("List");
        }
        [HttpPost]
        public ActionResult Edit(CProductViewModel inProd)
        {
            dbDemoContext db = new dbDemoContext();
            TProduct c = db.TProducts.FirstOrDefault(t => t.FId == inProd.FId);
            if (c != null)
            {
                if (inProd.photo != null)
                {
                    string pName = Guid.NewGuid().ToString() + ".jpg";
                    c.FImagePath = pName;
                    string path = _enviro.WebRootPath + "/images/" + pName;
                    inProd.photo.CopyTo(new FileStream(path, FileMode.Create));
                }
                c.FName = inProd.FName;
                c.FCost = inProd.FCost;
                c.FPrice = inProd.FPrice;
                c.FQty = inProd.FQty;
                
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

    }
}
