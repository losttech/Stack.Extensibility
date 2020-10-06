namespace LostTech.Stack.Extensibility.Services {
    using System;

    static class ServiceUtils {
        public static T Get<T>(this IServiceProvider serviceProvider) {
            if (serviceProvider is null) throw new ArgumentNullException(nameof(serviceProvider));

            return (T)serviceProvider.GetService(typeof(T));
        }
    }
}
