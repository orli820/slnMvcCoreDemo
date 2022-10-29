using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using prjMvcCoreDemo.Models;
using prjMvcCoreDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace prjMvcCoreDemo.Controllers
{
    public class ShoppingController : Controller
    {
        public IActionResult List()
        {
            var datas = from t in (new dbDemoContext()).TProducts
                        select t;
            List<CProductViewModel> list = new List<CProductViewModel>();
            foreach (TProduct t in datas)
            {
                CProductViewModel vm = new CProductViewModel();
                vm.product = t;
                list.Add(vm);
            }
            return View(list);
        }
        public IActionResult CartView( )
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_PRODUCTS))
            {
                string jsonCart = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCTS);
                List<CShoppingCartItem> cart = JsonSerializer.Deserialize<List<CShoppingCartItem>>(jsonCart);
                return View(cart);
            }
            else
                return RedirectToAction("List"); 
        }

        public IActionResult AddToCart(int? id)
        {

            dbDemoContext db = new dbDemoContext();
            TProduct prod = db.TProducts.FirstOrDefault(t => t.FId == id);
            if (prod == null)
                return RedirectToAction("List");
            return View(prod);
        }
        [HttpPost]
        public IActionResult AddToCart(CAddToCartViewModel vModel)
        {
            dbDemoContext db = new dbDemoContext();
            TProduct prod = db.TProducts.FirstOrDefault(t => t.FId == vModel.txtFid);
            if (prod == null)
                return RedirectToAction("List");
            string jsonCart = "";
            List<CShoppingCartItem> list = null;
            if (!HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_PRODUCTS))
                list = new List<CShoppingCartItem>();
            else
            {
                jsonCart = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCTS);
                list = JsonSerializer.Deserialize<List<CShoppingCartItem>>(jsonCart);
            }
            CShoppingCartItem item = new CShoppingCartItem()
            {
                count = vModel.txtCount,
                price = (decimal)prod.FPrice,
                productId = vModel.txtFid,
                product = prod
            };
            list.Add(item);
            jsonCart= JsonSerializer.Serialize(list);
            HttpContext.Session.SetString(CDictionary.SK_PURCHASED_PRODUCTS, jsonCart);
            return RedirectToAction("List");
        }
    }
}
