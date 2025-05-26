
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Rewrite;
using DotNetWebApp.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IPersonService, PersonService>();
var app = builder.Build();

app.MapGet("/", 
    (IPersonService personService) => 
    {
        return $"Hello, {personService.GetPersonName()}!";
    }
);
app.Run();