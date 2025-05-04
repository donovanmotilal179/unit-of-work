using ConsumeWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.Design.Serialization;
using System.Text;
using static System.Net.WebRequestMethods;

namespace ConsumeWebApi.Controllers
{
    public class ArmourController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7075/api");
        private readonly HttpClient _httpClient;

        public ArmourController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<ArmourViewMode>? armourList = new List<ArmourViewMode>();
            string lin = _httpClient.BaseAddress + "/Armour/Get";
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Armour/Get").Result;

            if(response.IsSuccessStatusCode) 
            { 
                string data = response.Content.ReadAsStringAsync().Result;
                armourList = JsonConvert.DeserializeObject<List<ArmourViewMode>>(data);
            }
            return View(armourList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ArmourViewMode model) 
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _httpClient.PostAsync(baseAddress + "/Armour/Create/CreateArmour", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Product Created.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex) 
            {
                TempData["errorMessage"] = ex.Message;
                return View();  
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                ArmourViewMode? armourViewMode = new ArmourViewMode();
                HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Armour/Get/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    armourViewMode = JsonConvert.DeserializeObject<ArmourViewMode>(data);
                }
                return View(armourViewMode);
            }
            catch (Exception ex) 
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }


        [HttpPost]
        public IActionResult Edit(ArmourViewMode model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _httpClient.PutAsync(baseAddress + "/Armour/Edit/UpdateArmour", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Product Updated.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id) 
        {
            try
            {
                ArmourViewMode? armourViewMode = new ArmourViewMode();
                HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Armour/Get/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    armourViewMode = JsonConvert.DeserializeObject<ArmourViewMode>(data);
                }
                return View(armourViewMode);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id) 
        {
            try
            {
                HttpResponseMessage response = _httpClient.DeleteAsync(_httpClient.BaseAddress + "/Armour/Delete/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Armour Deleted.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex) 
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();

        }
    }
}
