using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace EnjoyCodes.ViewComponentsLibrary
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
#if Release
                .UseUrls("http://*:7021")
#endif
                .UseStartup<Startup>();
    }
}
