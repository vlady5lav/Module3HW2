namespace ModuleHW
{
    public class ContactsService : IContactsService
    {
        private readonly IPhoneBook<IContact> _phoneBook;

        public ContactsService(IPhoneBook<IContact> phoneBook)
        {
            _phoneBook = phoneBook;
        }

        public void AddContact(string firstName = "", string lastName = "")
        {
            var contact = new Contact { FirstName = firstName, LastName = lastName };
            _phoneBook.Add(contact);
        }
    }
}
