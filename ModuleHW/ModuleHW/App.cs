using System;

namespace ModuleHW
{
    public class App
    {
        private readonly IPhoneBook<IContact> _phoneBook;

        public App(IPhoneBook<IContact> phoneBook)
        {
            _phoneBook = phoneBook;
        }

        public void Start()
        {
            _phoneBook.Add(new Contact() { FirstName = "Александр", LastName = "Суворов" });
            _phoneBook.Add(new Contact() { FirstName = "Алевтина", LastName = "Андреева" });
            _phoneBook.Add(new Contact() { FirstName = "Владислав", LastName = "Шевченко" });
            _phoneBook.Add(new Contact() { FirstName = "Иван", LastName = "Ярославский" });
            _phoneBook.Add(new Contact() { FirstName = "Иван", LastName = "Варецкий" });
            _phoneBook.Add(new Contact() { FirstName = "Иван", LastName = "Святецкий" });
            _phoneBook.Add(new Contact() { FirstName = "Ellie", LastName = "Leen" });
            _phoneBook.Add(new Contact() { FirstName = "Elsa", LastName = "Jean" });
            _phoneBook.Add(new Contact() { FirstName = "Emilia", LastName = "Clarke" });
            _phoneBook.Add(new Contact() { FirstName = "John", LastName = "Week" });
            _phoneBook.Add(new Contact() { FirstName = "John", LastName = "Travolta" });
            _phoneBook.Add(new Contact() { FirstName = "?Unknown", LastName = "Number" });
            _phoneBook.Add(new Contact() { FirstName = "Someone", LastName = "&Special" });
            _phoneBook.Add(new Contact() { FirstName = "12345", LastName = "67890" });
            _phoneBook.Add(new Contact() { FirstName = "01234", LastName = "56789" });
            _phoneBook.Add(new Contact() { FirstName = "98765", LastName = "43210" });
            _phoneBook.Add(new Contact() { FirstName = "*#06#*", LastName = string.Empty });
            _phoneBook.Add(new Contact() { FirstName = "!@#$%^&*()-_=+", LastName = string.Empty });
            _phoneBook.Add(new Contact() { FirstName = string.Empty, LastName = "Olofsen" });
            _phoneBook.Add(new Contact() { FirstName = string.Empty, LastName = "&&&777???Last" });
            _phoneBook.Add(new Contact() { FirstName = "&&&777???First", LastName = string.Empty });
            _phoneBook.Add(new Contact() { FirstName = "&999First", LastName = string.Empty });

            DisplayContacts("Ив");

            Console.ReadKey();
        }

        public void DisplayContacts(string name)
        {
            var contacts = _phoneBook[name];

            foreach (var contact in contacts)
            {
                Console.WriteLine(contact.FullName);
            }
        }
    }
}
