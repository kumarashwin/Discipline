using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Tache.Domain.Abstract;
using Tache.Domain.Entities;

namespace Tache.Controllers {
    public class ActivityController : ApiController {
        private IActivityRepository activityRepo;

        public ActivityController(IActivityRepository activityRepo) {
            this.activityRepo = activityRepo;
        }

        public IEnumerable<Activity> GetAll() => activityRepo.Activities;

        public Activity Get(int id) => activityRepo.Activities.Where(a => a.Id == id).First();

        public void Post(Activity activity) => activityRepo.CreateOrUpdate(activity);

        public void Put(Activity activity) => activityRepo.CreateOrUpdate(activity);

        public void Delete(int id) => activityRepo.Delete(id);
    }
}