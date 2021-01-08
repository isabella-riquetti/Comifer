using Comifer.Data.Context;
using System.Linq;
using Xunit;

namespace Comifer.Data.Test
{
    public class Tests
    {
        [Fact]
        public void CustomerTest()
        {
            using (var dbContext = new ComiferContext())
            {
                var firstCustomer = dbContext.Customer.FirstOrDefault();
                var customers = dbContext.Customer.ToList();

                Assert.NotNull(firstCustomer);
            }
        }
    }
}
