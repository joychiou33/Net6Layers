using ExamLayer.Data;
using ExamLayer.Repositories.Interface;
using ExamLayer.Repositories.Service;
using ExamLayer.Service;
using ExamLayer.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<BookStoreDbContext>(option => option.UseInMemoryDatabase("BookStoreDB"));
builder.Services.AddDbContext<MarketPlaceDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("MPConn")));

builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IIndexBusinessMetaService, IndexBusinessMetaService>();
builder.Services.AddScoped<ISettingPermissionService, SettingPermissionService>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DocumentTitle = "........";
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        c.InjectStylesheet("Your-optional-custom.css");

        //Show more of the model by default
        c.DefaultModelExpandDepth(2);

        //Close all of the major nodes
        c.DocExpansion(DocExpansion.None);

        //Show the example by default
        c.DefaultModelRendering(ModelRendering.Example);

        //Turn on Try it by default
        c.EnableTryItOutByDefault();

        //Performance Requirement - sorry. Highlighting kills javascript rendering on big json
        c.ConfigObject.AdditionalItems.Add("syntaxHighlight", false);

        //c.ConfigObject.AdditionalItems["syntaxHighlight"] = new Dictionary<string, object>
        //{
        //    ["activated"] = false
        //};
     }
     );
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
