using Xunit;
using Models;

namespace Tests;

public class StoreTest
{
    [Fact]
    public void CreateCustomerIDTest()
    {
        Customer customer = new Customer();
        customer.Id = 1;
        Assert.Equal("Customer ID", customer.Id);
    }

    [Fact]
    public void CreateCustomerDateTime()
    {
        Customer customer = new Customer();
        customer.DateCreated = DateTime.Now;
        Assert.Equal("Date Created", customer.DateCreated);
    } 
}