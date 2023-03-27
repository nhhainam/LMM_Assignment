using LMM_WebClient.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace LMM_WebClient.Controllers
{
    public class MaterialController : Controller
    {
        private IConfiguration configuration;
        private HttpClient client = null;
        private string apiurl = "";

        public MaterialController(IConfiguration _configuration)
        {
            configuration = _configuration;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            apiurl = configuration.GetValue<string>("PortUrl") + "api/Materials";

        }
        // GET: MaterialController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MaterialController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MaterialController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MaterialController/Create
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

        // GET: MaterialController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MaterialController/Edit/5
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
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
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

            return View();
        }

        // POST: MaterialController/Delete/5
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
