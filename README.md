# DotNetWebApp

[How to create project and File Structure](https://learn.microsoft.com/en-us/training/modules/build-your-first-aspnet-core-web-app/3-exercise-create-project?pivots=vscode)

[How to Run and Debug](https://learn.microsoft.com/en-us/training/modules/build-your-first-aspnet-core-web-app/4-exercise-run-project?pivots=vscode)

Run with Watch : [open the integrated terminal]
<img src="DotNetWebApp/image/ss1.png">

[Overview](https://learn.microsoft.com/en-us/training/modules/introduction-to-aspnet-core/3-how-aspnet-core-works)


### Middleware : 
When an ASP.NET Core app receives an HTTP request, it passes through a series of components that are responsible for processing the request and generating a response. These components are called middleware.

Each middleware can be thought of as terminal or nonterminal. Nonterminal middleware processes the request and then calls the next middleware in the pipeline. Terminal middleware is the last middleware in the pipeline and doesn't have a next middleware to call.

Delegates added with app.Run() are always terminal middleware. They don't call the next middleware in the pipeline. They're the last middleware component that runs. They only expect a HttpContext object as a parameter. app.Run() is a shortcut for adding terminal middleware.


```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("Hello from middleware 1. Passing to the next middleware!\r\n");

    // Call the next middleware in the pipeline
    await next.Invoke();

    await context.Response.WriteAsync("Hello from middleware 1 again!\r\n");
});

app.Run(async context =>
{
    await context.Response.WriteAsync("Hello from middleware 2!\r\n");
});

app.Run();

```
#### Built-in middleware
ASP.NET Core provides a set of built-in middleware components that you can use to add common functionality to your app. Example

```csharp

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
```

In the preceding code:

* app.UseExceptionHandler() adds a middleware component that catches exceptions and returns an error page.

* app.UseHsts() adds a middleware component that sets the Strict-Transport-Security header.

* app.UseHttpsRedirection() adds a middleware component that redirects HTTP requests to HTTPS.

* app.UseAntiforgery() adds a middleware component that prevents cross-site request forgery (CSRF) attacks.

* app.MapStaticAssets() and app.MapRazorComponents<App>() map routes to endpoints, which are then handled by the endpoint routing middleware. The endpoint routing middleware is implicitly added by the WebApplicationBuilder.

In this context, methods that start with Use are generally for mapping middleware. Methods that start with Map are generally for mapping endpoints.

There are many middleware bult in . [visit here](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-8.0&preserve-view=true)


### Exercise - Use built-in middleware

Your team lead tasked you to create a barebones website for your company. The website should display a welcome message on the main page, and display a brief history of the company on a separate /about page. A previous version of the app had the company history at the /history URL, so you need to redirect requests from /history to /about to maintain compatibility with existing links.

