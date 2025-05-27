
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Rewrite;
using DotNetWebApp.Services;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<IWelcomeService, WelcomeService>();

var app = builder.Build();

app.MapGet("/", async (IWelcomeService welcomeService1, IWelcomeService welcomeService2) => 
    {
        string message1 = $"Message1: {welcomeService1.GetWelcomeMessage()}";
        string message2 = $"Message2: {welcomeService2.GetWelcomeMessage()}";
        return $"{message1}\n{message2}";
    }
);

app.Run();