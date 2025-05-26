namespace DotNetWebApp.Services;

public class PersonService : IPersonService
{
    public string GetPersonName()
    {
        return "John Doe";
    }
}