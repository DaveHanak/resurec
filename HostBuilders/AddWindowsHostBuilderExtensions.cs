using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using resurec.ViewModels;

namespace resurec.HostBuilders
{
    public static class AddWindowsHostBuilderExtensions
    {
        public static IHostBuilder AddWindows(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton((s) => new MainWindow()
                {
                    DataContext = s.GetRequiredService<MainViewModel>()
                });
            });

            return hostBuilder;
        }
    }
}
