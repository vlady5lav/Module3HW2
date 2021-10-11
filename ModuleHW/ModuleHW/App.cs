using System;

namespace ModuleHW
{
    public class App
    {
        private readonly IContactsService _contactsService;
        private readonly IPhoneBook<IContact> _phoneBook;
        private readonly string _searchName;

        public App(IPhoneBook<IContact> phoneBook, IContactsService contactsService)
        {
            _contactsService = contactsService;
            _phoneBook = phoneBook;
            _searchName = "Ив";
        }

        public void Start()
        {
            ProvideContacts();

            DisplayContacts(_searchName);

            Console.ReadKey();
        }

        public void ProvideContacts()
        {
            _contactsService.AddContact("Александр", "Суворов");
            _contactsService.AddContact("Алевтина", "Андреева");
            _contactsService.AddContact("Василь", "Шевченко");
            _contactsService.AddContact("Василіса", "Петренко");
            _contactsService.AddContact("Владислав", "Шевченко");
            _contactsService.AddContact("Іван", "Нечай");
            _contactsService.AddContact("Ілларіон", "Чумка");
            _contactsService.AddContact("Ілля", "Батрак");
            _contactsService.AddContact("Иван", "Ярославский");
            _contactsService.AddContact("Иван", "Варецкий");
            _contactsService.AddContact("Иван", "Гілка");
            _contactsService.AddContact("Иван", "Яйцівка");
            _contactsService.AddContact("Иван", "Святецкий");
            _contactsService.AddContact("Ellie", "Leen");
            _contactsService.AddContact("Elsa", "Jean");
            _contactsService.AddContact("Emilia", "Clarke");
            _contactsService.AddContact("John", "Week");
            _contactsService.AddContact("John", "Travolta");
            _contactsService.AddContact("?Unknown", "Number");
            _contactsService.AddContact("Someone", "&Special");
            _contactsService.AddContact("12345", "67890");
            _contactsService.AddContact("01234", "56789");
            _contactsService.AddContact("98765", "43210");
            _contactsService.AddContact("*#06#*");
            _contactsService.AddContact("!@#$%^&*()-_=+");
            _contactsService.AddContact("Olofsen");
            _contactsService.AddContact("&&&777???Last");
            _contactsService.AddContact("&&&777???First");
            _contactsService.AddContact("&999First");
        }

        public void DisplayContacts(string name)
        {
            var contacts = _phoneBook[name];
            var newLine = Environment.NewLine;

            Console.WriteLine($"{newLine} Contacts starting with \"{name}\":{newLine}");

            foreach (var contact in contacts)
            {
                Console.WriteLine($" {contact.FullName} [{(contact as Contact).Collection}]");
            }
        }
    }
}
