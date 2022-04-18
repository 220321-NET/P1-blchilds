namespace UI;
public interface IMenu
{
    public Task Start();
    public Task Start(HttpService httpService);
    public Task Start(HttpService httpService, Customer value1);
}
