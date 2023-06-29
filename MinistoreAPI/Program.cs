using DataAccess;
using DataAccess.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using Repository;
using Repository.Implement;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

ODataConventionModelBuilder odataBuilder = new ODataConventionModelBuilder();
odataBuilder.EntitySet<Product>("Products");
odataBuilder.EntitySet<Invoice>("Invoices");
builder.Services.AddControllers().AddOData(options => options.Select().Filter()
.Count().OrderBy().Expand().SetMaxTop(null).AddRouteComponents("odata", odataBuilder.GetEdmModel()));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MiniStoreContext>(
  options => options.UseSqlServer("name=ConnectionStrings:Ministore"));
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<CategoryDAO>();
builder.Services.AddScoped<ProductDAO>();
builder.Services.AddScoped<InvoiceDAO>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
