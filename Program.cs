using DotnetApi.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(
    (options) =>
    {
        options.AddPolicy(
            "Development",
            (corsBuilder) =>
            {
                corsBuilder
                    .WithOrigins(
                        "http://localhost:4200",
                        "http://localhost:3000",
                        "http://localhost:8000"
                    )
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            }
        );
        options.AddPolicy(
            "Production",
            (corsBuilder) =>
            {
                corsBuilder
                    .WithOrigins("https://www.mysite.com")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            }
        );
    }
);
builder.Services.AddScoped<IUserRepository, UserRepository>();

// this builds the app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // for swagger ui
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
    app.UseCors("Development");
}
else
{ // for production use https
    app.UseHttpsRedirection();
    app.UseCors("Production");
}
app.MapControllers();
app.UseAuthorization();

// app.MapGet("/weatherforecast", () => { }).WithName("GetWeatherForecast").WithOpenApi();

// keeps server running
app.Run();
