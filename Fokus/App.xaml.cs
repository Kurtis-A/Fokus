using AutoMapper;
using Fokus.Data;
using Fokus.Helpers;
using Fokus.Repository;
using Fokus.Services;
using Fokus.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Fokus
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider ServiceProvider;

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();
            Mapper();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = ServiceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(@"Server=CORA\SQLEXPRESS;Database=Fokus;Trusted_Connection=True");
            });

            services.AddSingleton<MainWindow>();
            services.AddSingleton<Home>();
            services.AddSingleton<Views.Activity>();

            services.AddScoped<ActivityViewModel>();

            services.AddScoped<ActivityService>();

            services.AddScoped<ActivityRepository>();
        }

        private void Mapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Data.Activity, ActivityViewModel>();
            });

            Globals.Mapper = config.CreateMapper();
        }
    }
}