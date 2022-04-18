namespace UI;
using System.ComponentModel.DataAnnotations;

public class Collection : IMenu
{
    public async virtual Task Start() {}
    public async virtual Task Start(HttpService httpService) {}
    public async virtual Task Start(HttpService httpService, Customer value1) {}

    public string ReadStuff() 
    {
        TryAgain:
        string input = Console.ReadLine()!.Trim();

        if(String.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Invalid input, try again");
            goto TryAgain;
        }
        else
        {
            return input;
        }
    }
}