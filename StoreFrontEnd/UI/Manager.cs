namespace UI;

public class Manager : Collection 
{
    public override async Task Start(HttpService _httpService)
    {
        Console.WriteLine("88888888888888888888888888");
        Console.WriteLine("|| [1] View Inventory   ||");
        Console.WriteLine("|| [2] Update Inventory ||");
        Console.WriteLine("88888888888888888888888888");

        string input = ReadStuff();

        switch (input)
        {
            case "1":
                List<Product> inventoryList = await _httpService.GetInventoryAsync();
                ViewInventory(inventoryList);
                await new MenuFactory().GetMenu("manager").Start(_httpService);
                break;
            case "2":
                await chooseInventory(_httpService);
                break;
        }
    }
    public void ViewInventory(List<Product> inventoryList)
    {
        
        for(int i = 0; i < inventoryList.Count; i++)
        {
            Console.WriteLine("[" + $"{inventoryList[i].Id}" + "] " + $"{inventoryList[i].getName}" + ": " + $"{inventoryList[i].Amount}");
        }
    }

    public async Task chooseInventory(HttpService _httpService)
    {   
        string chooseProduct = "";
        
        do
        {
            Console.WriteLine("Choose inventory item by index to update, i to view inventory, or x to exit:");
            chooseProduct = ReadStuff();

            Product product;

            switch (chooseProduct)
            {
                case "1": 
                    product = new("Coffee");
                    await SendToDataLayer(_httpService, product, chooseProduct);
                    break;
                case "2":
                    product = new("Eggs");
                    await SendToDataLayer(_httpService, product, chooseProduct);
                    break;
                case "3":
                    product = new("Steak");
                    await SendToDataLayer(_httpService, product, chooseProduct);
                    break;
                case "4":
                    product = new("French Toast");
                    await SendToDataLayer(_httpService, product, chooseProduct);
                    break;
                case "5":
                    product = new("Pancakes");
                    await SendToDataLayer(_httpService, product, chooseProduct);
                    break;
                case "6":
                    product = new("Cherry Pie");
                    await SendToDataLayer(_httpService, product, chooseProduct);
                    break;
                case "i":
                    List<Product> inventoryList = await _httpService.GetInventoryAsync();
                    ViewInventory(inventoryList);
                    break;
            }
        } while(chooseProduct != "x");
    }

    public async Task SendToDataLayer(HttpService _httpService, Product product, string chooseProduct)
    {
        string addAmount = "";

        Console.WriteLine("Enter the amount of inventory:");
        addAmount = ReadStuff();

        product.Id = Convert.ToInt32(chooseProduct);
        product.Amount = Convert.ToInt32(addAmount);
        await _httpService.SetDatabaseInventoryAsync(product);      
    }
}