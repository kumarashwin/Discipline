using Newtonsoft.Json;
using System;
using System.Web.Mvc;
using Tache.Infrastructure.Attributes;
using Tache.Models.Abstract;

namespace Tache.Controllers {
    public class DayController : Controller {
        private IDaysViewModelRepository daysViewModelRepository;

        public DayController(IDaysViewModelRepository daysViewModelRepository) {
            this.daysViewModelRepository = daysViewModelRepository;
        }

        [ByDefaultReturnView]
        public ActionResult Index(int year, int month, int day) => 
            Content(JsonConvert.SerializeObject(daysViewModelRepository.Days(new DateTime(year, month, day))), "application/json");
    }
}