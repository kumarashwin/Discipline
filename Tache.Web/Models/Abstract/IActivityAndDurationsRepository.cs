using System.Collections.Generic;

namespace Tache.Models {
    public interface IActivityAndDurationsRepository {

        ICollection<ActivityAndDurationsViewModel> Model();
        ActivityAndDurationsRepository For(string activity);
    }
}