using System.Collections.Generic;
using System.Web.Http;
using Tache.Domain.Abstract;
using Tache.Domain.Entities;

namespace Tache.Web.Controllers {
    public class ActivityController : ApiController {
        private IActivityRepository activityRepo;

        public ActivityController(IActivityRepository activityRepo) {
            this.activityRepo = activityRepo;
        }

        public IEnumerable<Activity> GetAll() => activityRepo.Activities;

        public Activity Get(int id) => activityRepo.Retrieve(id);

        public void Post(Activity activity) => activityRepo.CreateOrUpdate(activity);

        public void Put(Activity activity) => activityRepo.CreateOrUpdate(activity);
    }
}