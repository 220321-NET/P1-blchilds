using Xunit;
using Models;

namespace Tests;

public class UnitTest1
{
    [Fact]
    public void CreateCustomerTest()
    {
        Customer customer = new Customer();
        customer.Id = 1;
        Assert.Equal("Customer ID", customer.Id);
    }
}