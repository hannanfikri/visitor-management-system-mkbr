using Abp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Visitor.Configuration;
using Visitor.Seed;
using Visitor.Web;

namespace Visitor.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class VisitorDbContextFactory : IDesignTimeDbContextFactory<VisitorDbContext>
    {
        public VisitorDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<VisitorDbContext>();
            var seedDataGenerator = new SeedDataGenerator();

            /*
             You can provide an environmentName parameter to the AppConfigurations.Get method. 
             In this case, AppConfigurations will try to read appsettings.{environmentName}.json.
             Use Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") method or from string[] args to get environment if necessary.
             https://docs.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli#args
             */
            var configuration = AppConfigurations.Get(
                WebContentDirectoryFinder.CalculateContentRootFolder(),
                addUserSecrets: true
            );

            VisitorDbContextConfigurer.Configure(builder, configuration.GetConnectionString(VisitorConsts.ConnectionStringName));

            return new VisitorDbContext(builder.Options);
        }
    }
}
