using Microsoft.AspNetCore.Mvc;
using BL;
using Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreBL _bl;
        public StoreController(IStoreBL bl)
        {
            _bl = bl;
        }
        
        [HttpGet("{userName}")]
        public async Task<Customer> GetCustomerAsync(string userName)
        {
            Customer customer = new Customer();

            customer = await _bl.FindCustomerAsync(userName);
            return customer;
        }

        [HttpPut("Cost")]
        public async Task<int> PutCostOfItemsInCartAync(Cart value)
        {
            return await _bl.CostOfItemsInCartAsync(value);
        }
       
        [HttpGet("Cart/{id}")]
        public async Task<List<Cart>> GetOrderHistoryAsync(int id)
        {
            return await _bl.GetOrderHistoryAsync(id);          
        }

        [HttpGet("Inventory")]
        public async Task<List<Product>> GetInventoryAsync()
        {
            return await _bl.GetInventoryAsync();
        }

        [HttpPost("CreateCustomer/{customerName}")]
        public async Task Post(string customerName)
        {
            await _bl.CreateCustomerAsync(customerName);
        }

        [HttpPost("PlaceOrder")]
        public async Task PostOrderAsync(Tuple<Cart, Customer, int> OrderToBePlaced)
        {
            Cart cart = new Cart();
            Customer customer = new Customer();
            int cost;

            cart = OrderToBePlaced.Item1;
            customer = OrderToBePlaced.Item2;
            cost = OrderToBePlaced.Item3;

            await _bl.PlaceOrderAsync(cart, customer, cost);
        }

        [HttpPut("SetInventory")]
        public async Task PutInventoryAsync(Product product)
        {
            await _bl.SetDatabaseInventoryAsync(product);
        }

        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        // DELETE api/<StoreController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
