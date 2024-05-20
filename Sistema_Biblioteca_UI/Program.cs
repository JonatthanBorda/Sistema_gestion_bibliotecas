using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Sistema_Biblioteca.Services;
using Sistema_Biblioteca_UI;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//Se agrega la URL base que apunta a la API:
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7220/") });
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IDocTypeService, DocTypeService>();

builder.Services.AddSweetAlert2();

await builder.Build().RunAsync();
