namespace UI;


public class StoreMenu : Collection {
    
    public override async Task Start(HttpService _httpService, Customer value) 
    {
        Cart cart = new Cart();
        List<Product> inventoryList = await _httpService.GetInventoryAsync();
        string input;

        Console.WriteLine("88888888888888888888888888888888888888888888888888888888888888888888888");
        Console.WriteLine("|| [1] Coffee $2.00                     [4] French Toast $7.00       ||");
        Console.WriteLine("|| [2] 2 Eggs, Any Style $4.00          [5] Pancakes $6.00           ||");
        Console.WriteLine("|| [3] Steak $10.00                     [6] Famous Cherry Pie $3.00  ||");
        Console.WriteLine("|| [r] to Review                        [x] to Exit                  ||");                    
        Console.WriteLine("88888888888888888888888888888888888888888888888888888888888888888888888");
        
        do
        {   
            Console.WriteLine("Enter a number to order, r to see order history, or x to Exit:");
            input = ReadStuff();

            switch (input)
            {
                case "1":
                    Product coffee = new Product("Coffee");
                    await AddToCart(coffee, cart, input, inventoryList, _httpService);
                    break;
                case "2":
                    Product eggs = new Product("Eggs");
                    await AddToCart(eggs, cart, input, inventoryList, _httpService);
                    break;
                case "3":
                    Product steak = new Product("Steak");
                    await AddToCart(steak, cart, input, inventoryList, _httpService);
                    break;
                case "4":
                    Product frenchtoast = new Product("French Toast");
                    await AddToCart(frenchtoast, cart, input, inventoryList, _httpService);
                    break;
                case "5":
                    Product pancakes = new Product("Pancakes");
                    await AddToCart(pancakes, cart, input, inventoryList, _httpService);
                    break;
                case "6":
                    Product cherrypie = new Product("Cherry Pie");
                    await AddToCart(cherrypie, cart, input, inventoryList, _httpService);
                    break;
                case "r":
                    List<Cart> orderHistory = await _httpService.GetOrderHistoryAsync(value.Id);
                    for (int i = 0; i < orderHistory.Count; i++)
                    {
                        Console.WriteLine("Order " + $"[{i + 1}]: " + orderHistory[i].Cost + "$");
                    }
                    continue;
            }
        } while (input != "x");
        
        //
        // DON'T FORGET THIS IS HERE. 
        //

        if (cart.currentCart.Count > 0)
        {
            await SendOrder(cart, _httpService, value);
        }
        else
        {
            Console.WriteLine("Returning to main menu");
            await new MenuFactory().GetMenu("main").Start(_httpService);
        }
    }
    public async Task<Cart> AddToCart(Product value, Cart cart, string itemID, List<Product> inventoryList, HttpService _httpService)
    {   
        ChooseAmount:
        Console.WriteLine("How many orders of " + $"{value.getName}" + " would you like?");
        string input = ReadStuff();

        value.Amount = Convert.ToInt32(input);
        value.Id = Convert.ToInt32(itemID);

        if (inventoryList.Exists(x => x.getName == value.getName)) 
            {
                int itemIndex = inventoryList.FindIndex(x => x.getName == value.getName);

                if (value.Amount > inventoryList[itemIndex].Amount)
                {
                    Console.WriteLine("Not enough stock, there are " + $"{inventoryList[itemIndex].Amount}" + " orders of " + $"{inventoryList[itemIndex].getName}" + " left");
                    goto ChooseAmount;
                }
                else
                {
                    Product product = new Product(value.getName);
                    product.Id = itemIndex + 1;
                    product.Amount = inventoryList[itemIndex].Amount - value.Amount;
                    await _httpService.SetDatabaseInventoryAsync(product);
                    
                    cart.currentCart.Add(value);        
                    Console.WriteLine($"{value.getName} " + "added to cart");
                }
        }
        return cart;
    }
    public async Task SendOrder(Cart cart, HttpService _httpService, Customer value)
    {   
        int cost = await _httpService.CostOfItemsInCartAsync(cart);
        
        Console.WriteLine("Your order total is: " + $"{cost}" + "$");
        Console.WriteLine("Do you wish to place this order? [1] Yes [2] No"); // review order and remove product maybe
        string input = ReadStuff();

        if (input == "1")
        {
            Console.WriteLine("Thank you for shopping at Double R diner! Your order will be delivered shortly. Returning to main menu");
            await _httpService.PlaceOrderAsync(cart, value, cost);
            await new MenuFactory().GetMenu("main").Start(_httpService);
        }
        else
        {
            Console.WriteLine("Returning to main menu");
            await new MenuFactory().GetMenu("main").Start(_httpService);
        }
        
    }
}