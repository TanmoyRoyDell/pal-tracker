using System.Linq;
using Steeltoe.Common.HealthChecks;
using static Steeltoe.Common.HealthChecks.HealthStatus;

namespace PalTracker
{
    public class TimeEntryHealthContributor : IHealthContributor
    {
        private ITimeEntryRepository _repo;
        public int MaxCount = 5;

        public TimeEntryHealthContributor(ITimeEntryRepository repo)
        {
            _repo = repo;
        }

        public string Id 
        { 
            get
            {
                return "timeEntry";
            }
        }

        public HealthCheckResult Health()
        {
            var count = _repo.List().Count();
            var status = count < MaxCount ? UP : DOWN;

            var health = new HealthCheckResult {Status = status};

            health.Details.Add("threshold", MaxCount);
            health.Details.Add("count", count);
            health.Details.Add("status", status.ToString());

            return health;
        }
    }
}