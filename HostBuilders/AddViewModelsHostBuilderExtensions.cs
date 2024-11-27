using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using resurec.Services;
using resurec.Stores;
using resurec.ViewModels;

namespace resurec.HostBuilders
{
    public static class AddViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddTransient((s) => CreateRecordingHistoryViewModel(s));
                services.AddSingleton<Func<RecordingHistoryViewModel>>((s) => () => s.GetRequiredService<RecordingHistoryViewModel>());
                services.AddSingleton<NavigationService<RecordingHistoryViewModel>>();

                services.AddTransient<ResurecViewModel>();
                services.AddSingleton<Func<ResurecViewModel>>((s) => () => s.GetRequiredService<ResurecViewModel>());
                services.AddSingleton<NavigationService<ResurecViewModel>>();

                services.AddSingleton<MainViewModel>();
            });

            return hostBuilder;
        }

        private static RecordingHistoryViewModel CreateRecordingHistoryViewModel(IServiceProvider services)
        {
            return RecordingHistoryViewModel.LoadViewModel(
                services.GetRequiredService<RecorderStore>(),
                services.GetRequiredService<NavigationService<ResurecViewModel>>());
        }
    }
}
