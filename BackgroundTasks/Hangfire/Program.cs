using Hangfire.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Hangfire;
using Hangfire.SqlServer;
using Hangfire.Models.Entities;
using Hangfire.Infrastructures.Service;
using Hangfire.HangfireAuthorization;
using Hangfire.HangfireHosted;
using Hangfire.Infrastructures.Abstraction;
using Hangfire.Dashboard;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddDefaultUI()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//Add Config and Register IHostedService For RecurringJob  
//builder.Services.AddHostedService<StartRecurringJob>();
#region Hangfire

//Configuration For Hangfire

builder.Services.AddHangfire(config =>
config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
.UseSimpleAssemblyNameTypeSerializer()
.UseRecommendedSerializerSettings()
.UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection")));
//Add HangfireServer Services
builder.Services.AddHangfireServer(options =>
{
    options.SchedulePollingInterval = TimeSpan.FromMinutes(1);

    //create New Queue
    options.Queues = new[] { "default", "create_queue1" };
});

#endregion

#region Service
builder.Services.AddSingleton<SmsService>();
builder.Services.AddSingleton<EmailService>();
builder.Services.AddSingleton<ISmsIocService, SmsIocService>();
builder.Services.AddSingleton<IChangeDisplayNameJob, ChangeDisplayNameJob>();
builder.Services.AddSingleton<ICreateQueue, CreateQueue>();
#endregion

//Global Retray Job Configuration
GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 2, DelaysInSeconds = new[]{ 10, 5 } });


var app = builder.Build();

//Set ApplicationLifeTime For RecurringJob

app.Lifetime.ApplicationStarted.Register(RecurringJob);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
app.MapRazorPages();

//Middleware-Hangfire
app.UseHangfireDashboard(pathMatch: "/hangfire", options: new DashboardOptions
{
    Authorization = new[]
    {
        new HangfireAuthorizationFiltercs(),
    },

    DashboardTitle = "داشبورد تسک های پس زمینه ",

    //Change DisplayName Jobs
    //DisplayNameFunc = (ctx, job) => $"{job.Type.Name}-->{job.Method.Name} : {job.Method.IsPublic}",

    //ReadOnly Dashboard
   // IsReadOnlyFunc = (DashboardContext context) => true 
});

app.Run();

#region RecurringJob Method
void RecurringJob()
{
    Hangfire.RecurringJob.AddOrUpdate<EmailService>("Article-LifeTimeApp", p => p.SendArticlesToUsers("Classicus.ma@gmail.com"), Cron.Minutely());
}
#endregion