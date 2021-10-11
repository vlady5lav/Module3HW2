using System;

namespace ModuleHW
{
    public class Contact : IContact
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Collection { get; set; }
        public string FullName
        {
            get
            {
                if (!string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName))
                {
                    return $"{FirstName} {LastName}";
                }
                else if (!string.IsNullOrEmpty(FirstName) && string.IsNullOrEmpty(LastName))
                {
                    return $"{FirstName}";
                }
                else if (string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName))
                {
                    return $"{LastName}";
                }
                else
                {
                    throw new ArgumentNullException("First Name and Last Name are null or empty!");
                }
            }
        }
    }
}
