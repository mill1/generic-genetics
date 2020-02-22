using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace ConsoleUI
{
    public static class ConsoleServiceProvider
    {
        public static IServiceCollection RegisterAppComponents(this IServiceCollection collection)
        {
            collection
                .AddScoped<GenericGenetics.Interfaces.IUICircleEvolution, UICircleEvolution>()
                .AddScoped<GenericGenetics.UI.Runner>();

            return collection;
        }
    }
}
