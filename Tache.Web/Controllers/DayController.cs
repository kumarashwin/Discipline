using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Web.Mvc;
using Tache.Models.Abstract;

namespace Tache.Controllers {
    public class DayController : Controller {
        private IDaysViewModelRepository daysViewModelRepository;

        public DayController(IDaysViewModelRepository daysViewModelRepository) {
            this.daysViewModelRepository = daysViewModelRepository;
        }

        public ViewResult Index(string id) => View(model: JsonConvert.SerializeObject(daysViewModelRepository.Days(id)));

        //public ActionResult Index(string id) => Content(JsonConvert.SerializeObject(daysViewModelRepository.Days(id)), "application/json");
    }
}