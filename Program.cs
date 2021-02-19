using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace _502StartupError
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var hostbuilder = CreateHostBuilder(args).Build();

                /// set a time bomb to exit after 1 minute
                /// this produces a 502.3.12030 for a request in 
                /// progress when it happens 
                /// 
                //Task.Run(async () =>
                //{
                //    await Task.Delay(60 * 1000);
                //    Process.GetCurrentProcess().Kill();
                //});
                hostbuilder.Run();
            }
            catch { }
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
