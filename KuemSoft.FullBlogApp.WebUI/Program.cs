using FluentValidation.AspNetCore;
using KuemSoft.FullBlogApp.Repository.Extensions.Microsoft;
using KuemSoft.FullBlogApp.Service.Extensions.Microsoft;
using KuemSoft.FullBlogApp.Service.Utilities.FluentValidation.ValidationRulesForArticleDTOs;
using System.Globalization;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddFluentValidation(opt =>
{
    opt.RegisterValidatorsFromAssemblyContaining<ArticleCreateDtoValidator>();
    opt.DisableDataAnnotationsValidation = true;
    opt.ValidatorOptions.LanguageManager.Culture = new CultureInfo("tr");
});

builder.Services.AddDependenciesToRepositoryLayer(builder.Configuration);
builder.Services.AddDependenciesForServiceLayer();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
        name: "Admin",
        areaName: "Admin",
        pattern: "Admin/{controller=Home}/{action=Index}/{id?}"

        );
    endpoints.MapDefaultControllerRoute();
});

app.Run();
