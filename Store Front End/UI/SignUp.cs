namespace UI;
using System.ComponentModel.DataAnnotations;

public class SignUp : Collection
{
    public async override Task Start(HttpService _httpService)
    {
        Console.WriteLine("Enter a username:");
        string customerName = ReadStuff();
            
        await _httpService.CreateCustomerAsync(customerName);
        await new MenuFactory().GetMenu("main").Start(_httpService);
    }
}