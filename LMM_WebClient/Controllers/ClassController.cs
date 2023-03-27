﻿using LMM_WebClient.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using static System.Net.WebRequestMethods;

namespace LMM_WebClient.Controllers
{
    public class ClassController : Controller
    {
        private IConfiguration configuration;
        private HttpClient client = null;
        private string apiurl = "";

        public ClassController(IConfiguration _configuration)
        {
            configuration = _configuration;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            apiurl = configuration.GetValue<string>("PortUrl") + "api/Classes";

        }
        // GET: ClassController
        public async Task<IActionResult> Index()
		{
			List<Class> items = new List<Class>();
			String userId = (String)HttpContext.Session.GetString("userId");
            string apiEndpoint = apiurl + "/GetClassesByUserId?" + "userId=" + userId;

			HttpResponseMessage response = await client.GetAsync(apiEndpoint);
            string strData = await response.Content.ReadAsStringAsync();


			if (strData != null)
			{
				items = JsonConvert.DeserializeObject<List<Class>>(strData);

			}
            return View(items);
        }

        // GET: ClassController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            Class item = new Class();
			string apiEndpoint = apiurl + "/" + id;
            String materialUrl = "https://localhost:5000/api/Materials/getbyclass/" + id;
            HttpResponseMessage responeMaterial = await client.GetAsync(materialUrl);
            //HttpResponseMessage response = await client.GetAsync(apiEndpoint);
			//string strData = await response.Content.ReadAsStringAsync();
            string materialData = await responeMaterial.Content.ReadAsStringAsync();
            List<Material> listMaterial = JsonConvert.DeserializeObject<List<Material>>(materialData);
            ViewBag.ListMaterial = listMaterial;
            ViewBag.classId = id;

            //         if (strData != null)
            //{
            //	item = JsonConvert.DeserializeObject<Class>(strData);

            //}

            return View(item);
        }

        // GET: ClassController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClassController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClassController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ClassController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClassController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ClassController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
