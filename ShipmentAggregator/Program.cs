var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ICacheService, CacheService>();
builder.Services.AddScoped<CompanyAService>();
builder.Services.AddScoped<CompanyBService>();
builder.Services.AddScoped<CompanyCService>();
builder.Services.AddScoped<DeliveryCompanyServiceFactory>();
builder.Services.Configure<Redis>(builder.Configuration.GetSection("Redis"));

builder.Services.AddStackExchangeRedisCache(options =>
{
    var redis = builder.Configuration.GetSection("Redis").Get<Redis>();
    options.Configuration = redis?.ConnectionString;
    options.InstanceName = redis?.Instance;
});

builder.Services.AddControllers();
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

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
