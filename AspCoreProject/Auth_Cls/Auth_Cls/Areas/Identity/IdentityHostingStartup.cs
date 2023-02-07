using System;
using Auth_Cls.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Auth_Cls.Areas.Identity.IdentityHostingStartup))]
namespace Auth_Cls.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            //builder.ConfigureServices((context, services) => {
            //    //services.AddDbContext<Auth_ClsContext>(options =>
            //    //    options.UseSqlServer(
            //    //        context.Configuration.GetConnectionString("Auth_ClsContextConnection")));

            //    //services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    //    .AddEntityFrameworkStores<Auth_ClsContext>();
            //});
        }
    }
}