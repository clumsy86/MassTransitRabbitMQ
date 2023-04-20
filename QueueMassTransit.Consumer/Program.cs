using MassTransit;
using QueueMassTransit.Consumer;
using SharedMessage;

var builder = WebApplication.CreateBuilder(args);
var settingsRabbitMQ  = builder.Configuration.GetSection("RabbitMQ:Username");

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:44433")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                      });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<MessageConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("myrabbit", h =>
        {
            h.Username(builder.Configuration.GetSection("RabbitMQ:Username").Value);
            h.Password(builder.Configuration.GetSection("RabbitMQ:Password").Value);
        });

        cfg.ReceiveEndpoint("order-service", e =>
        {
            e.ConfigureConsumer<MessageConsumer>(context);
        });
    });
});
builder.Services.AddSignalR();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors();

app.UseRouting();



//app.UseAuthorization();

app.MapHub<Hubs>("/hubs/notifications");

app.Run();
