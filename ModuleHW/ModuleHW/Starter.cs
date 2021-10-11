using Microsoft.Extensions.DependencyInjection;

namespace ModuleHW
{
    public class Starter
    {
        public void Run()
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<ICultureIdentifier, CultureIdentifier>()
                .AddTransient<IPhoneBook<IContact>, PhoneBook<IContact>>()
                .AddTransient<App>()
                .BuildServiceProvider();

            var app = serviceProvider.GetService<App>();
            app.Start();
        }
    }
}
