namespace Models;

public class Cart : CommonData
{
    public Cart() {}
    public List<Product> currentCart { get; set; } = new List<Product>();
    public int Cost { get; set; }
}