using Fokus.Data;
using Fokus.Helpers;
using Fokus.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fokus.Services
{
    public class ActivityService
    {
        private readonly ActivityRepository _repository;

        public ActivityService(ActivityRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ActivityViewModel>> FetchActivities()
        {
            var result = await _repository.FindAll();
            return Globals.Mapper.Map<List<Activity>, List<ActivityViewModel>>(result);
        }
    }
}