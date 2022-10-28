using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Visitor.EntityFrameworkCore;

namespace Visitor.HealthChecks
{
    public class VisitorDbContextHealthCheck : IHealthCheck
    {
        private readonly DatabaseCheckHelper _checkHelper;

        public VisitorDbContextHealthCheck(DatabaseCheckHelper checkHelper)
        {
            _checkHelper = checkHelper;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            if (_checkHelper.Exist("db"))
            {
                return Task.FromResult(HealthCheckResult.Healthy("VisitorDbContext connected to database."));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("VisitorDbContext could not connect to database"));
        }
    }
}
