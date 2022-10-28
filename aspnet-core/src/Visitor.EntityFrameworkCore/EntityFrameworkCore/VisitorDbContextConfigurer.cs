using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Visitor.EntityFrameworkCore
{
    public static class VisitorDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<VisitorDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<VisitorDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}