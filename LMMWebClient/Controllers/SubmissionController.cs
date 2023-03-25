using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMMWebClient.Controllers
{
    public class SubmissionController : Controller
    {
        // GET: SubmissionController
        public ActionResult Index()
        {
            return View();
        }

        // GET: SubmissionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SubmissionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SubmissionController/Create
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

        // GET: SubmissionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SubmissionController/Edit/5
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

        // GET: SubmissionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SubmissionController/Delete/5
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
