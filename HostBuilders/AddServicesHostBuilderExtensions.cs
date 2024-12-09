using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using resurec.DbContexts.Factories;
using resurec.Services.RecordingCreators;
using resurec.Services.RecordingEditors;
using resurec.Services.RecordingProviders;
using resurec.Services.RecordingRemovers;

namespace resurec.HostBuilders
{
    public static class AddServicesHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((hostContext, services) =>
            {
                string connectionString = hostContext.Configuration.GetConnectionString("Default")!;
                services.AddSingleton<IResurecDbContextFactory>(new ResurecDbContextFactory(connectionString));
                services.AddSingleton<IRecordingProvider, DatabaseRecordingProvider>();
                services.AddSingleton<IRecordingCreator, DatabaseRecordingCreator>();
                services.AddSingleton<IRecordingEditor, DatabaseRecordingEditor>();
                services.AddSingleton<IRecordingRemover, DatabaseRecordingRemover>();
            });

            return hostBuilder;
        }
    }
}
