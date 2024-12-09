using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using resurec.Stores;

namespace resurec.HostBuilders
{
    public static class AddStoresHostBuilderExtensions
    {
        public static IHostBuilder AddStores(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<RecorderStore>();
                services.AddSingleton<NavigationStore>();
            });

            return hostBuilder;
        }
    }
}
