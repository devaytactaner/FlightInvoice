using FlightInvoice.BackgroundServices;
using FlightInvoice.BackgroundServices.Service;
using FlightInvoice.BackgroundServices.Service.IService;
using FlightInvoice.BackgroundServices.Utility;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "Invoice Listener";
});

SD.FlightApiBase = builder.Configuration["ServiceUrls:FlightApi"];

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IFlightService, FlightService>();
builder.Services.AddTransient<IBaseService, BaseService>();
builder.Services.AddTransient<IFlightService, FlightService>();

builder.Services.AddHostedService<Worker>();

var defaultOptions = new ConfigurationFile();
builder.Configuration.GetSection("Options").Bind(defaultOptions);
builder.Services.AddSingleton(defaultOptions);

//builder.Services.AddScoped<IFlightService, FlightService>();

var host = builder.Build();

host.Run();
