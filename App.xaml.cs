using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTK.Graphics.OpenGL;
using resurec.DbContexts;
using resurec.DbContexts.Factories;
using resurec.HostBuilders;
using resurec.Models;
using resurec.Models.ResourceMonitors;
using resurec.Services;
using resurec.Services.RecordingCreators;
using resurec.Services.RecordingProviders;
using resurec.Services.RecordingRemovers;
using resurec.Stores;
using resurec.ViewModels;

namespace resurec
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .AddServices()
                .AddModels()
                .AddViewModels()
                .AddStores()
                .AddWindows()
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            IResurecDbContextFactory resurecDbContextFactory = _host.Services.GetRequiredService<IResurecDbContextFactory>();
            using (ResurecDbContext dbContext = resurecDbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }

            NavigationService<ResurecViewModel> navigationService = _host.Services.GetRequiredService<NavigationService<ResurecViewModel>>();
            navigationService.Navigate();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            GlobalTimer globalTimer = _host.Services.GetRequiredService<GlobalTimer>();
            globalTimer.Start();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            GlobalTimer globalTimer = _host.Services.GetRequiredService<GlobalTimer>();
            globalTimer.Stop();

            _host.Dispose();
            base.OnExit(e);
        }
    }

}
