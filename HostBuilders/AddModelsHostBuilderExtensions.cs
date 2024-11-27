using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using resurec.Models.ResourceMonitors;
using resurec.Models;
using resurec.Services;
using resurec.Stores;
using resurec.ViewModels;

namespace resurec.HostBuilders
{
    public static class AddModelsHostBuilderExtensions
    {
        public static IHostBuilder AddModels(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<GlobalTimer>();
                services.AddSingleton<HardwareMonitor>();
                services.AddSingleton<SnapshotCache>();
                services.AddSingleton<RecordingHistory>();
                services.AddSingleton<Recorder>();
            });
            return hostBuilder;
        }
    }
}
