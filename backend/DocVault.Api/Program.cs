
using dotenv.net;
using DocVault.Api.Services;
DotEnv.Load();
var builder = WebApplication.CreateBuilder(args);


builder.Configuration.AddEnvironmentVariables();
builder.Services
    .AddControllers()
    .AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<BlobService>();
builder.Services.AddSingleton<CosmosService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
