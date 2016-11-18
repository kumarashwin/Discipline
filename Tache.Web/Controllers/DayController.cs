using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web.Mvc;
using Tache.Models.Abstract;

namespace Tache.Controllers {
    public class DayController : Controller {
        private IDaysViewModelRepository daysViewModelRepository;

        public DayController(IDaysViewModelRepository daysViewModelRepository) {
            this.daysViewModelRepository = daysViewModelRepository;
        }

        public ActionResult Index(int year, int month, int day) {
            var result = JsonConvert.SerializeObject(daysViewModelRepository.Days(new DateTime(year, month, day)));
            if (ControllerContext.HttpContext.Request.AcceptTypes.Contains("application/json")) {
                return Content(result, "application/json");
            }
            return View(model: result);
        } 

        //public ViewResult Index(int year, int month, int day) => View(model: JsonConvert.SerializeObject(daysViewModelRepository.Days(new DateTime(year, month, day))));

        //public ActionResult Index(string id) => Content(JsonConvert.SerializeObject(daysViewModelRepository.Days(id)), "application/json");
    }
}