using LMMWebClient.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http.Headers;

namespace LMMWebClient.Controllers
{
    public class AssignmentController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly HttpClient client = null;
        private string AssignmentApiUrl = "";
        public AssignmentController(IConfiguration _configuration)
        {
            configuration = _configuration;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            var portUrl = configuration["PortUrl"];
            AssignmentApiUrl = portUrl + "api/Assignments";
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(AssignmentApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            dynamic temp = JObject.Parse(strData);
            var lst = temp.value;
            List<Assignment> items = ((JArray)temp.value).Select(x => new Assignment
            {
                //AssignmentId = (int)x["Id"],
                //Author = (string)x["Author"],
                //ISBN = (string)x["ISBN"],
                //Title = (string)x["Title"],
                //Price = (decimal)x["Price"]
            }).ToList();

            return View(items);
        }

        // GET: AssignmentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AssignmentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AssignmentController/Create
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

        // GET: AssignmentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AssignmentController/Edit/5
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

        // GET: AssignmentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AssignmentController/Delete/5
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
