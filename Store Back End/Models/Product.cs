namespace Models;
using System.Text.Json.Serialization;

public class Product : CommonData
{
    private string _productName = "";
    private int _productAmount = 0;

    public Product(string product)
    {
        _productName = product;
    }

    public Product() {}

    [JsonConstructorAttribute]
    public Product(string getName, int Amount)
    {
        _productName = getName;
        _productAmount = Amount;
    }

    public string getName
    {
        get => _productName;
        set
        {
            _productName = value;
        }
    }

    public int Amount 
    { 
        get => _productAmount; 
        set
        {
            _productAmount = value;
        } 
    }
}
