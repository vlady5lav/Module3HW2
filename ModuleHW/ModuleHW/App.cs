using System;

namespace ModuleHW
{
    public class App
    {
        private readonly IPhoneBook<IContact> _phoneBook;
        private readonly string _searchName;

        public App(IPhoneBook<IContact> phoneBook)
        {
            _phoneBook = phoneBook;
            _searchName = "Іл";
        }

        public void Start()
        {
            AddContact("Александр", "Суворов");
            AddContact("Алевтина", "Андреева");
            AddContact("Василь", "Шевченко");
            AddContact("Василіса", "Петренко");
            AddContact("Владислав", "Шевченко");
            AddContact("Іван", "Нечай");
            AddContact("Ілларіон", "Чумка");
            AddContact("Ілля", "Батрак");
            AddContact("Иван", "Ярославский");
            AddContact("Иван", "Варецкий");
            AddContact("Иван", "Гілка");
            AddContact("Иван", "Святецкий");
            AddContact("Ellie", "Leen");
            AddContact("Elsa", "Jean");
            AddContact("Emilia", "Clarke");
            AddContact("John", "Week");
            AddContact("John", "Travolta");
            AddContact("?Unknown", "Number");
            AddContact("Someone", "&Special");
            AddContact("12345", "67890");
            AddContact("01234", "56789");
            AddContact("98765", "43210");
            AddContact("*#06#*");
            AddContact("!@#$%^&*()-_=+");
            AddContact(string.Empty, "Olofsen");
            AddContact(string.Empty, "&&&777???Last");
            AddContact("&&&777???First");
            AddContact("&999First");

            DisplayContacts(_searchName);

            Console.ReadKey();
        }

        public void AddContact(string firstName)
        {
            _phoneBook.Add(new Contact() { FirstName = firstName, LastName = string.Empty });
        }

        public void AddContact(string firstName, string lastName)
        {
            _phoneBook.Add(new Contact() { FirstName = firstName, LastName = lastName });
        }

        public void DisplayContacts(string name)
        {
            var contacts = _phoneBook[name];
            var newLine = Environment.NewLine;

            Console.WriteLine($"{newLine}Contacts starting with \"{name}\":{newLine}");

            foreach (var contact in contacts)
            {
                Console.WriteLine($"{contact.FullName} [{(contact as Contact).Collection}]");
            }
        }
    }
}
