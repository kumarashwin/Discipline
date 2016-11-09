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

        public ActionResult Index(string id) => Content(JsonConvert.SerializeObject(daysViewModelRepository.Days(id)), "application/json");
    }
}