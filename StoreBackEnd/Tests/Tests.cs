using Xunit;
using Models;
using System.ComponentModel.DataAnnotations;

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

    [Fact]
    public void CreateCustomerName()
    {
        Customer customer = new Customer();
        customer.Name = "Brandon";
        Assert.Equal("Brandon", customer.Name);
    }

    [Fact]
    public void CreateCustomerNameNotVoid()
    {
        Customer customer = new Customer();

        Assert.Throws<ValidationException>(() => customer.Name = "");
    }


}