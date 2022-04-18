namespace UI;

public class MenuFactory
{
    HttpService httpService = new HttpService();
    public IMenu GetMenu(string menuString)
    {
        switch (menuString)
        {
            case "main":
                return new MainMenu(httpService);
            case "login":
                return new Login();
            case "signup":
                return new SignUp();
            case "manager":
                return new Manager();
            case "storemenu":
                return new StoreMenu();
            default:
                return new MainMenu(httpService);
        }
    }
}