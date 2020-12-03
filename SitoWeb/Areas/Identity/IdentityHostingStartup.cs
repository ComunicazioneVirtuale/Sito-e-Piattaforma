using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SitoWeb.Areas.Identity.Data;
using SitoWeb.Data;

[assembly: HostingStartup(typeof(SitoWeb.Areas.Identity.IdentityHostingStartup))]
namespace SitoWeb.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<UserDBContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("UserDBContextConnection")));

                services.AddDefaultIdentity<SitoWebUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<UserDBContext>();
            });
        }
    }
}