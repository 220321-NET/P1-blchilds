namespace UI;

public class Login : Collection
{
    public async override Task Start(HttpService _httpService)
    {
        Console.WriteLine("Enter your username:");
        string input = ReadStuff();

        Customer customer =  await _httpService.FindCustomerAsync(input);

        if (input == customer.Name) 
        {
            Console.WriteLine("Welcome " + $"{customer.Name}" + ", please choose from the following menu:");
            await new MenuFactory().GetMenu("storemenu").Start(_httpService, customer);
        }
        // else
        // {
        //     Console.WriteLine("That username does not exist. Please sign up here:");
        //     new MenuFactory().GetMenu("signup").Start(_httpService);
        // }  
    }
}
