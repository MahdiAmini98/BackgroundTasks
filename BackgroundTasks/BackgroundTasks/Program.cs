using Domain.BackgroundTasks.MyQueue;
using Domain.BackgroundTasks.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Service
builder.Services.AddHostedService<NewPostEmailToAllUsersTimedHostedService>();

//Cahnnel
MyQueue myQueue = new();
Producer producer = new(myQueue._channel);
Consumer consumer = new(myQueue._channel);
await Task.WhenAll(producer.SendMessage(), consumer.ReceviceMessage());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
