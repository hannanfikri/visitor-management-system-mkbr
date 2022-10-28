using System.Data.SqlClient;
using Shouldly;
using Xunit;

namespace Visitor.Tests.General
{
    // ReSharper disable once InconsistentNaming
    public class ConnectionString_Tests
    {
        [Fact]
        public void SqlConnectionStringBuilder_Test()
        {
            var csb = new SqlConnectionStringBuilder("Server=localhost; Database=Visitor; Trusted_Connection=True;");
            csb["Database"].ShouldBe("Visitor");
        }
    }
}
