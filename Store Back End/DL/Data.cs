using Microsoft.Data.SqlClient;
using System.Data;

namespace DL;

public class Data : IData
{
    private readonly string _connectionString;

    public Data(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public async Task<Customer> FindCustomerAsync(string userName)
    {
        // initialize variables and open connection
        Customer customer = new Customer();
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        // make sql call to database to select customer based on name passed into method
        SqlCommand cmd = new SqlCommand("SELECT * FROM Customer WHERE Customer.Name = @Name;", connection);
        cmd.Parameters.AddWithValue("@Name", userName);

        // if the customer is in the database, create an object and assign a name and id then close connections
        SqlDataReader dataReader = cmd.ExecuteReader();
        if(await dataReader.ReadAsync())
        {
            int Id = dataReader.GetInt32(0);
            customer.Name = userName;
            customer.Id = Id;
        }
        dataReader.Close();
        connection.Close();        

        // return customer to called program
        return customer;
    }

    public async Task CreateCustomerAsync(string customerName) 
    {
        // initialize variables and open connection
        using SqlConnection connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        // put the created customer into the customer table
        using SqlCommand cmd = new SqlCommand("INSERT INTO Customer(Name, Pass) VALUES (@Name, @Pass)", connection);
        cmd.Parameters.AddWithValue("@Name", customerName);
        cmd.Parameters.AddWithValue("@Pass", "ifIgetaroundtoit");

        // execute command and close connection
        await cmd.ExecuteScalarAsync();
        await connection.CloseAsync();
    }

    public async Task<int> CostOfItemsInCartAsync(Cart value)
    {   
        int cost = 0;

        using SqlConnection connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        for(int i = 0; i < value.currentCart.Count; i++)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Product WHERE Product.Name = @name", connection);
            cmd.Parameters.AddWithValue("@name", value.currentCart[i].getName);

            SqlDataReader dataReader = await cmd.ExecuteReaderAsync();
            
            if (await dataReader.ReadAsync())
            {
                int add = dataReader.GetInt32(4);
                cost += (add * value.currentCart[i].Amount);
            }
            await dataReader.CloseAsync();
        }
        await connection.CloseAsync();

        return cost;
    }

    public async Task<List<Product>> GetInventoryAsync()
    {   
        List<Product> inventoryList = new List<Product>();

        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        SqlCommand cmd = new SqlCommand("SELECT * FROM Inventory JOIN Product ON Inventory.InventoryId = Product.InventoryID", connection);
        
        SqlDataReader dataReader = cmd.ExecuteReader();

        while(await dataReader.ReadAsync())
        {   
            int Id = (dataReader.GetInt32(2));
            int productAmount = dataReader.GetInt32(5);
            string inventoryItem = dataReader.GetString(4);
            Product product = new Product(inventoryItem);
            product.Id = Id;
            product.Amount = productAmount;
            inventoryList.Add(product);
        }
        dataReader.Close();
        connection.Close();

        return inventoryList;
    }

    public async Task SetDatabaseInventoryAsync(Product value)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        SqlCommand cmd = new SqlCommand("SELECT * FROM Product", connection);
        
        cmd = new SqlCommand("UPDATE Product SET Quantity = @IUpdate WHERE ProductId = @id", connection);

        cmd.Parameters.AddWithValue("@IUpdate", value.Amount);
        cmd.Parameters.AddWithValue("@id", value.Id);

        await cmd.ExecuteScalarAsync();

        cmd = new SqlCommand("SELECT * FROM Product WHERE ProductID = " + $"{value.Id}", connection);
        SqlDataReader dataReader = await cmd.ExecuteReaderAsync();

        if (await dataReader.ReadAsync())
        {
            value.Amount = dataReader.GetInt32(3);
        }
        dataReader.Close();
        connection.Close();       
    }

    public async Task PlaceOrderAsync(Cart cart, Customer customer, int cost)
    {
        // initialize variables and open connection
        int cartID = 0;
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        // creates a cart to put ordered items into and adds the total cost of the cart
        SqlCommand cmd = new SqlCommand("SELECT * FROM Cart INSERT INTO Cart(CustomerID, Total) VALUES (@CustomerID, @Total)", connection);
        cmd.Parameters.AddWithValue("@CustomerID", customer.Id);
        cmd.Parameters.AddWithValue("@Total", cost);
        await cmd.ExecuteScalarAsync();

        // gets the cart id to know which cart to put the items into
        cmd = new SqlCommand("SELECT TOP 1 * FROM Cart ORDER BY CartID DESC", connection);
        SqlDataReader dataReader = cmd.ExecuteReader();

        while(await dataReader.ReadAsync())
        {
            cartID = dataReader.GetInt32(0);
        }
        dataReader.Close();

        // place items into the newly made cart
        for(int i = 0; i < cart.currentCart.Count; i++)
        {
            cmd = new SqlCommand("SELECT * FROM Orders INSERT INTO Orders(ProductID, CartID) VALUES (@ProductID, @CartID)", connection);
            cmd.Parameters.AddWithValue("@ProductID", cart.currentCart[i].Id);
            cmd.Parameters.AddWithValue("@CartID", cartID);
            await cmd.ExecuteScalarAsync();
        }
        connection.Close();
    }

    public async Task<List<Cart>> GetOrderHistoryAsync(int customer) 
    {
        // initializing variables
        List<Cart> orderHistory = new List<Cart>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        // grabs a list of all the carts the customer has purchased
        SqlCommand cmd = new SqlCommand("SELECT * FROM Cart WHERE CustomerID = @Id", connection);
        cmd.Parameters.AddWithValue("@Id", customer);
        cmd.ExecuteScalar();

        // loops through all the carts purchased by the customer, grabs the total cost of each and prints them to console. count is being used to keep count of the number of carts.
        SqlDataReader dataReader = cmd.ExecuteReader();
        while(await dataReader.ReadAsync())
        {   
            int costOfCart = dataReader.GetInt32(2);
            Cart cart = new Cart();
            cart.Cost = costOfCart;
            orderHistory.Add(cart);
        }

        // closing connections and returning
        dataReader.Close();
        connection.Close();

        return orderHistory;
    }
}
    

