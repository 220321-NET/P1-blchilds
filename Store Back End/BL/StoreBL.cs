namespace BL;
using DL;

public class StoreBL : IStoreBL
{
    private readonly IData _repo;
    public StoreBL(IData repo)
    {
        _repo = repo;
    }
    public async Task<Customer> FindCustomerAsync(string userName) 
    {
        return await _repo.FindCustomerAsync(userName);
    }
    public async Task CreateCustomerAsync(string customerName) 
    {
        await _repo.CreateCustomerAsync(customerName);
    }

    public async Task<int> CostOfItemsInCartAsync(Cart value)
    {
        return await _repo.CostOfItemsInCartAsync(value);
    }

    public async Task<List<Product>> GetInventoryAsync()
    {
        return await _repo.GetInventoryAsync();
    }

    public async Task SetDatabaseInventoryAsync(Product value)
    {
        await _repo.SetDatabaseInventoryAsync(value);
    }

    public async Task PlaceOrderAsync(Cart cart, Customer customer, int cost)
    {
        await _repo.PlaceOrderAsync(cart, customer, cost);
    }

    public async Task<List<Cart>> GetOrderHistoryAsync(int value)
    {
        return await _repo.GetOrderHistoryAsync(value);
    }
}

