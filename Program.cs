using Microsoft.EntityFrameworkCore;
using CareerSharp.Models;
using CareerSharp.Services;
using System.Text;

var AllowFrontendRequests = "_allowFrontendRequests";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowFrontendRequests,
        policy =>
        {
            policy.WithOrigins("http://localhost:8001");
        });
});


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpClient<JobListingsService>();
builder.Services.AddSingleton<IJobListingsService, JobListingsService>();
builder.Services.AddDbContext<JobListingContext>(opt => opt.UseInMemoryDatabase("JobBoard"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(AllowFrontendRequests);

app.UseAuthorization();

app.MapControllers();

app.Run();
