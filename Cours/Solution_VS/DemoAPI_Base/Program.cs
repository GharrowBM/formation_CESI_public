using DemoAPI_Base.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<FakeDbService>();

builder.Services.AddTransient<IGuidGeneratorService, GuidGeneratorService>();
builder.Services.AddScoped<IGuidGeneratorServiceBis, GuidGeneratorServiceBis>();
builder.Services.AddSingleton<IGuidGeneratorServiceTer, GuidGeneratorServiceTer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
