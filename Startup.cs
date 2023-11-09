using System;
using EFTutorial.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EFTutorial
{

    /// <summary>
    ///     Used for registration of new interfaces
    /// </summary>
    internal class Startup
    {
        public IServiceProvider ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            // Add new lines of code here to register any interfaces and concrete services you create
            services.AddTransient<IMainService, MainService>();

            return services.BuildServiceProvider();
        }
    }
}