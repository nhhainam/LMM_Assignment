using LMM_WebClient.Entity;
using LMM_WebClient.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;

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


			if (!strData.Equals("[]"))
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

			HttpResponseMessage response = await client.GetAsync(apiEndpoint);
			string strData = await response.Content.ReadAsStringAsync();


			if (strData != null)
			{
				item = JsonConvert.DeserializeObject<Class>(strData);

			}

			return View(item);
        }

		// GET: ClassController/Details/5
		public async Task<IActionResult> Search(string classCode, string userId)
		{
            List<ClassDTO> items = null;
			if (string.IsNullOrEmpty(classCode))
			{
				return View(items);
			}
			string apiEndpoint = apiurl + "/Search?" + "classCode=" + classCode + "&userId=" + Int32.Parse(userId);

			HttpResponseMessage response = await client.GetAsync(apiEndpoint);
			string strData = await response.Content.ReadAsStringAsync();


			if (!strData.Equals("[]"))
			{
				items = JsonConvert.DeserializeObject<List<ClassDTO>>(strData);

			}

			return View(items);
		}

        public async Task<ActionResult> JoinClass(string userId, string classId)
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
