namespace Models;
using System.ComponentModel.DataAnnotations;

public class Customer : CommonData
{
    public Customer() {}
    private string name = "";
    public string Name 
    { 
        get => name; 
        set
        {
            if(String.IsNullOrWhiteSpace(value))
            {
                throw new ValidationException("Name cannot be empty");
            }
            name = value;
        } 
    }
}
