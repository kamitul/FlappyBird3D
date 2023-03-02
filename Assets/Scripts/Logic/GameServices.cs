using Services;
using Services.Assets;
using Services.Save;
using System.Collections.Generic;

namespace Logic
{
    public static class GameServices
    {
        private static readonly List<IService> services = new List<IService>()
        {
            new OnlineAssetService(),
            new SaveService()
        };

        public static T GetService<T>()
            where T : class, IService
        {
            return services.Find(x => x is T) as T;
        }
    }
}
