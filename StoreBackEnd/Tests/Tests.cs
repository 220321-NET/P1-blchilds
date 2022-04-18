using Xunit;
using Models;

namespace Tests;

public class StoreTest
{
    [Fact]
    public void CreateCustomerID()
    {
        Customer customer = new Customer();
        customer.Id = 1;
        Assert.Equal(1, customer.Id);
    }

    // [Fact]
    // public void CreateCustomerDateTime()
    // {
    //     Customer customer = new Customer();
    //     customer.DateCreated = DateTime.Now;
    //     Assert.Equal(customer.DateCreated, customer.DateCreated);
    // }

    [Fact]
    public void CreateCustomerName()
    {
        Customer customer = new Customer();
        customer.Name = "Brandon";
        Assert.Equal("Brandon", customer.Name);
    }


}