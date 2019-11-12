﻿using Des.Blazor.Msal.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MsalConfigurationExtensions
    {
        public static IServiceCollection AddAzureActiveDirectory(this IServiceCollection services, Func<IServiceProvider, Task<IMsalConfig>> configurator)
        {
            services.AddScoped<IConfigProvider>(sp => 
            {
                return new DelegateConfigProvider(sp, configurator);
            });

            services.AddScoped<MsalAuthenticationStateProvider>();
            services.AddScoped<AuthenticationStateProvider>(s =>
            {
                return s.GetService<MsalAuthenticationStateProvider>();
            });

            return services;
        }
    }
}
