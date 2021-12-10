using DeliveryConsumer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryConsumer.Controllers
{
    public class ShopController : Controller
    {
        // GET: ShopController
        public async Task<ActionResult> Index()
        {
            List<Shop> shopList = new List<Shop>();

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://localhost:44356/api/Shops");
                string strValue = await response.Content.ReadAsStringAsync();
                shopList = JsonConvert.DeserializeObject<List<Shop>>(strValue);

            }

            return View(shopList);
        }

        // GET: ShopController/Details/5
        public ActionResult Details(int id)
        {
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Shop s)
        {
            s.ShopId = 0;
            using (var httpClient = new HttpClient())
            {
                string jsonInString = JsonConvert.SerializeObject(s);
                var response = await httpClient.PostAsync("https://localhost:44356/api/Shops"
                    , new StringContent(jsonInString, Encoding.UTF8, "application/json"));

            }
            TempData["ShopCreateMsg"] = "New Shop Created Successfully";
            return RedirectToAction("Index");
        }

        // GET: ShopController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Shop t_shop = new Shop();
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://localhost:44356/api/Shops/" + id.ToString());
                string strValue = await response.Content.ReadAsStringAsync();
                t_shop = JsonConvert.DeserializeObject<Shop>(strValue);

                TempData["OldShopName"] = t_shop.Name;

            }
            return View(t_shop);
        }

        // POST: ShopController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Shop e_shop)
        {
            if (((string)TempData["OldShopName"]) == e_shop.Name)
            {
                TempData["ShopEditNCMsg"] = "Shop Informations Not Changed";
            }
            else
            {
                using (var httpClient = new HttpClient())
                {
                    string jsonInString = JsonConvert.SerializeObject(e_shop);

                    var response = await httpClient.PutAsync("https://localhost:44356/api/Shops/" + id.ToString()
                        , new StringContent(jsonInString, Encoding.UTF8, "application/json"));
                    TempData["ShopEditMsg"] = "Shop Informations Updated";
                }
            }
            return RedirectToAction("Index");
        }

        // GET: ShopController/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync("https://localhost:44356/api/Shops/" + id.ToString());
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    TempData["ShopDeleteErrorMsg"] = "Delete Error: Shop is involved in a Delivery";
                }
                else
                {
                    TempData["ShopDeleteMsg"] = "Shop Deleted";
                }
            }

            return RedirectToAction("Index");

        }
    }
}
