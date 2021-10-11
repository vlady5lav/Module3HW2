using Microsoft.Extensions.DependencyInjection;

namespace ModuleHW
{
    public class Starter
    {
        public void Run()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IPhoneBook<IContact>, PhoneBook<IContact>>()
                .AddTransient<IContactsService, ContactsService>()
                .AddTransient<ICultureService, CultureService>()
                .AddTransient<App>()
                .BuildServiceProvider();

            var app = serviceProvider.GetService<App>();
            app.Start();
        }
    }
}
