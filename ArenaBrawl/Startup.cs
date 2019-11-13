using ArenaBrawl.Areas.Identity;
using ArenaBrawl.Data;
using ArenaBrawl.InMemoryData;
using ArenaBrawl.InMemoryData.Matchmaking;
using ArenaBrawl.Payments;
using ArenaBrawl.Services;
using Microsoft.ApplicationInsights.Extensibility.EventCounterCollector;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ArenaBrawl
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddLogging();
            services
                .AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>
                >();
            services.AddSingleton<StandardBrawlQueue>();
            services.AddSingleton<HistoricBrawlQueue>();
            services.AddSingleton<PlayerCountRepository>();
            services.AddSingleton<PlayerInGameCountRepository>();
            services.AddSingleton<PaymentService>();
            services.AddScoped<PlayerSession>();
            services.AddScoped<CircuitHandler, PlayerCountCircuitHandler>();
            services.AddApplicationInsightsTelemetry();
            services.ConfigureTelemetryModule<EventCounterCollectionModule>((module, o) =>
                {
                    module.Counters.Add(new EventCounterCollectionRequest("HistoricQueueSource", "PlayerJoinedHistoricQueue"));
                    module.Counters.Add(new EventCounterCollectionRequest("StandardQueueSource", "PlayerJoinedStandardQueue"));
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}