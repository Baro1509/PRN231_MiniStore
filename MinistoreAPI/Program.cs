using DataAccess;
using DataAccess.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.ModelBuilder;
using Repository;
using Repository.Implement;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

ODataConventionModelBuilder odataBuilder = new ODataConventionModelBuilder();
odataBuilder.EntitySet<Product>("Products");
odataBuilder.EntitySet<Invoice>("Invoices");
odataBuilder.EntitySet<Attendance>("Attendances");
odataBuilder.EntitySet<WorkShift>("WorkShifts");
odataBuilder.EntitySet<Duty>("Duties");
builder.Services.AddControllers().AddOData(options => options.Select().Filter()
.Count().OrderBy().Expand().SetMaxTop(null).AddRouteComponents("odata", odataBuilder.GetEdmModel()));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MiniStoreContext>(
  options => options.UseSqlServer("name=ConnectionStrings:MinistoreVy"));
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
builder.Services.AddScoped<IStaffRepository, StaffRepository>();
builder.Services.AddScoped<IWorkShiftRepository, WorkShiftRepository>();
builder.Services.AddScoped<IDutyRepository, DutyRepository>();
builder.Services.AddScoped<CategoryDAO>();
builder.Services.AddScoped<ProductDAO>();
builder.Services.AddScoped<InvoiceDAO>();
builder.Services.AddScoped<AttendanceDAO>();
builder.Services.AddScoped<DutyDAO>();
builder.Services.AddScoped<WorkShiftDAO>();
builder.Services.AddScoped<StaffDAO>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
