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
    public class DeliveryController : Controller
    {
        // GET: DeliveryController
        public async Task<ActionResult> Index()
        {
            List<Delivery> deliveryList = new List<Delivery>();
                
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync("https://localhost:44356/api/Deliveries");
                    string strValue = await response.Content.ReadAsStringAsync();
                    deliveryList = JsonConvert.DeserializeObject<List<Delivery>>(strValue);

                }
            
            return View(deliveryList);
        }

        // GET: DeliveryController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Delivery delivery = new Delivery();
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://localhost:44356/api/Deliveries/" + id.ToString());
                string strValue = await response.Content.ReadAsStringAsync();
                delivery = JsonConvert.DeserializeObject<Delivery>(strValue);

            }
            TempData["delivery_customer_id"] = delivery.CustomerId;
            return View(delivery);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://localhost:44356/api/Customers/");
                string strValue2 = await response.Content.ReadAsStringAsync();
                ViewBag.customers = JsonConvert.DeserializeObject<List<Customer>>(strValue2);

                response = await httpClient.GetAsync("https://localhost:44356/api/Shops/");
                string strValue3 = await response.Content.ReadAsStringAsync();
                ViewBag.shops = JsonConvert.DeserializeObject<List<Shop>>(strValue3);
            }
                return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Delivery d)
        {
            d.DeliveryId = 0;
            using (var httpClient = new HttpClient())
            {
                string jsonInString = JsonConvert.SerializeObject(d);
                var response = await httpClient.PostAsync("https://localhost:44356/api/Deliveries",
                    new StringContent(jsonInString, Encoding.UTF8, "application/json"));
                
            }
            TempData["DeliveryCreateMsg"] = "New Delivery Added";

            return RedirectToAction("Index");
        }

        // GET: DeliveryController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Delivery delivery = new Delivery();
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://localhost:44356/api/Deliveries/" + id.ToString());
                string strValue = await response.Content.ReadAsStringAsync();
                delivery = JsonConvert.DeserializeObject<Delivery>(strValue);

                response = await httpClient.GetAsync("https://localhost:44356/api/Customers/");
                string strValue2 = await response.Content.ReadAsStringAsync();
                ViewBag.customers = JsonConvert.DeserializeObject<List<Customer>>(strValue2);

                response = await httpClient.GetAsync("https://localhost:44356/api/Shops/");
                string strValue3 = await response.Content.ReadAsStringAsync();
                ViewBag.shops = JsonConvert.DeserializeObject<List<Shop>>(strValue3);

                TempData["OldDeliveryCustomerId"] = delivery.CustomerId;
                TempData["OldDeliveryShopId"] = delivery.ShopId;
                TempData["OldDeliveryArriveDate"] = delivery.ArriveDate;
                TempData["OldDeliveryStatus"] = delivery.Status;
            }
            return View(delivery);
        }

        // POST: DeliveryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Delivery d)
        {

            if (((int)TempData["OldDeliveryCustomerId"]) == d.CustomerId
                && ((int)TempData["OldDeliveryShopId"]) == d.ShopId
                && ((DateTime)TempData["OldDeliveryArriveDate"]) == d.ArriveDate
                && ((string)TempData["OldDeliveryStatus"]) == d.Status)
            {
                TempData["DeliveryEditNCMsg"] = "Delivery Informations Not Changed";
            }
            else { 
            
                using (var httpClient = new HttpClient())
                {
                    string jsonInString = JsonConvert.SerializeObject(d);

                    var response = await httpClient.PutAsync("https://localhost:44356/api/Deliveries/" + id.ToString()
                        , new StringContent(jsonInString, Encoding.UTF8, "application/json"));
                    TempData["DeliveryEditMsg"] = "Delivery Informations Updated";
                }
            }
            return RedirectToAction("Details", new { id = d.DeliveryId });
        }


        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
 
            int c_id = (int)TempData["delivery_customer_id"];     

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync("https://localhost:44356/api/Deliveries/" + id.ToString());
            }
            TempData["DeliveryDeleteMsg"] = "Delivery Informations Deleted With Products";
            return RedirectToAction("Details", "Customer",new { id=c_id});
        }

        [HttpGet]
        public IActionResult AddProduct(int id)
        {

            using (var httpClient = new HttpClient())
            {
      
                ViewBag.d_id = id;

            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product p)
        {
           

            using (var httpClient = new HttpClient())
            {
   
                string jsonInString = JsonConvert.SerializeObject(p);
                var response = await httpClient.PostAsync("https://localhost:44356/api/Products"
                    , new StringContent(jsonInString, Encoding.UTF8, "application/json"));

                TempData["DeliveryAddProductMsg"] = "New Prodcut Added Successfully";
            }
            
            return RedirectToAction("Details",new { id=p.DeliveryId});
        }

        
        public async Task<ActionResult> EditProduct(int id)
        {
            Product p = new Product();
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://localhost:44356/api/Products/" + id.ToString());
                string strValue = await response.Content.ReadAsStringAsync();
                p = JsonConvert.DeserializeObject<Product>(strValue);

                TempData["OldProductPrice"] = p.Price;
                TempData["OldProductName"] = p.Name;
            }
            return View(p);
        }

        // POST: DeliveryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditProduct(int id, Product p)
        {
            if (((int)TempData["OldProductPrice"]) == p.Price && ((string)TempData["OldProductName"]) == p.Name)
            {
                TempData["DeliveryEditProductNCMsg"] = "Product Informations Not Changed";
            }
            else { 
                using (var httpClient = new HttpClient())
                {
                    string jsonInString = JsonConvert.SerializeObject(p);

                    var response = await httpClient.PutAsync("https://localhost:44356/api/Products/" + id.ToString()
                        , new StringContent(jsonInString, Encoding.UTF8, "application/json"));
                    TempData["DeliveryEditProductMsg"] = "Product Informations Updated";
                }
            }
            int d_id = (int)TempData["delivery_id"];
            return RedirectToAction("Details",new { id=d_id});
        }

        [HttpGet]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            int d_id = (int)TempData["delivery_id"];
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync("https://localhost:44356/api/Products/" + id.ToString());
            }
            TempData["DeliveryDeleteProductMsg"] = "Product Removed From Delivery";
            return RedirectToAction("Details",new { id=d_id} );

        }

        public async Task<ActionResult> Filter(Query q)
        {
            if (q.status == null)
            {
                return RedirectToAction("Index");
            }
            else
            { 
                List<Delivery> deliveryList = new List<Delivery>();

                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync("https://localhost:44356/api/Deliveries/GetDeliveriesByStatus/" + q.status);
                    string strValue = await response.Content.ReadAsStringAsync();
                    deliveryList = JsonConvert.DeserializeObject<List<Delivery>>(strValue);

                }
                return View("../Delivery/Index", deliveryList);
            }
            

        }

    }
}
