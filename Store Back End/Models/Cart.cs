namespace Models;

public class Cart : CommonData
{
    private int _cost = 0;
    
    public Cart() {}
    public List<Product> currentCart { get; set; } = new List<Product>();

    public int Cost 
    { 
        get => _cost; 
        set
        {
            _cost = value;
        } 
    }
}