using Microsoft.Extensions.DependencyInjection;
using Zaaby.Common;
using Zaaby.ThreeTier.Abstractions.BusinessLogic;

namespace Zaaby.ThreeTier
{
    public static partial class ZaabyIServiceCollectionExtensions
    {
        public static IServiceCollection AddBll(this IServiceCollection services) =>
            services.Register<IBll>(ServiceLifetime.Scoped)
                .Register<BllAttribute>(ServiceLifetime.Scoped);
        
        public static IServiceCollection AddMessageHandler(this IServiceCollection services) =>
            services.Register<IMessageHandler>(ServiceLifetime.Scoped)
                .Register<MessageHandler>(ServiceLifetime.Scoped);
    }
}