namespace DL;

public interface IData
{
        public Task<Customer> FindCustomerAsync(string userName);
        public Task CreateCustomerAsync(string customerName);
        public Task<int> CostOfItemsInCartAsync(Cart value);
        public Task<List<Product>> GetInventoryAsync();
        public Task SetDatabaseInventoryAsync(Product value);
        public Task PlaceOrderAsync(Cart cart, Customer customer, int cost);
        public Task<List<Cart>> GetOrderHistoryAsync(int value);
}