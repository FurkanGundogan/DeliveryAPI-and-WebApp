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
    public class CustomerController : Controller
    {
        // GET: CustomerController

        public async Task<ActionResult> Index()
        {
       
            List<Customer> customerList = new List<Customer>();
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://localhost:44356/api/Customers");
                string strValue = await response.Content.ReadAsStringAsync();
                customerList = JsonConvert.DeserializeObject<List<Customer>>(strValue);

            }
            return View(customerList);
        }

        // GET: CustomerController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Customer customer = new Customer();
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://localhost:44356/api/Customers/"+id.ToString());
                string strValue = await response.Content.ReadAsStringAsync();
                customer = JsonConvert.DeserializeObject<Customer>(strValue);

            }
            return View(customer);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Customer c)
        {
            
            c.CustomerId = 0;
            using (var httpClient = new HttpClient())
            {
                string jsonInString = JsonConvert.SerializeObject(c);
                var response = await httpClient.PostAsync("https://localhost:44356/api/Customers"
                    , new StringContent(jsonInString, Encoding.UTF8, "application/json"));

            }
            TempData["CustomerAddMsg"] = "New Customer has been created successfully";
            return RedirectToAction("Index");
        }

        // GET: CustomerController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Customer customer = new Customer();
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://localhost:44356/api/Customers/" + id.ToString());
                string strValue = await response.Content.ReadAsStringAsync();
                customer = JsonConvert.DeserializeObject<Customer>(strValue);
                TempData["OldCustomerFirstName"] = customer.FirstName;
                TempData["OldCustomerLastName"] = customer.LastName;
                TempData["OldCustomerPhoneNumber"] = customer.PhoneNumber;
            }
            return View(customer);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Customer c)
        {
            if (((string)TempData["OldCustomerFirstName"])==c.FirstName
                && ((string)TempData["OldCustomerLastName"])==c.LastName
                && ((int)TempData["OldCustomerPhoneNumber"]) == c.PhoneNumber) {
                TempData["CustomerEditNCMsg"] = "Customer Informations Not Changed";
            }
            else { 
            using (var httpClient = new HttpClient())
                {
                    string jsonInString = JsonConvert.SerializeObject(c);
                    var response = await httpClient.PutAsync("https://localhost:44356/api/Customers/"+id.ToString()
                        , new StringContent(jsonInString, Encoding.UTF8, "application/json"));

                }
                TempData["CustomerEditMsg"] = "Customer Informations Updated";
            }
            
            return RedirectToAction("Details",new { id=c.CustomerId});
        }

        // POST: CustomerController/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
           
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync("https://localhost:44356/api/Customers/" + id.ToString());
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) {
                    TempData["CustomerDeleteErrorMsg"] = "Delete Error: Customer Has Delivery";
                }
                else
                {
                    TempData["CustomerDeleteMsg"] = "Customer Deleted";
                }
            }
            
            return RedirectToAction("Index");
                      
        }
    }
}
