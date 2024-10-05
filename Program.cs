var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// this builds the app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // for swagger ui
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{ // for production use https
    app.UseHttpsRedirection();
}
app.MapControllers();
app.UseAuthorization();

// app.MapGet("/weatherforecast", () => { }).WithName("GetWeatherForecast").WithOpenApi();

// keeps server running
app.Run();
