using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBookApp.Model
{
    public class ContactBook
    {
        #region Fields
        private List<Contact> contacts;
        #endregion

        #region Properties
        public List<Contact> Contacts { get; set; }
        #endregion

        #region Constructors
        public ContactBook()
        {
            Contacts = new List<Contact>()
            {
                new Contact(){Name = "abdz", PhoneNumber="2453679872"},
                new Contact(){Name = "abinaya", PhoneNumber="8936740923"},
                new Contact(){Name = "abinaya", PhoneNumber="8936740923"},
                new Contact(){Name = "abinaya", PhoneNumber="8936740923"},
                new Contact(){Name = "abinaya", PhoneNumber="8936740923"},
                new Contact(){Name = "abinaya", PhoneNumber="8936740923"},
                new Contact(){Name = "abinaya", PhoneNumber="8936740923"},
                new Contact(){Name = "abinaya", PhoneNumber="8936740923"},
                new Contact(){Name = "abinaya", PhoneNumber="8936740923"},
                new Contact(){Name = "abinaya", PhoneNumber="8936740923"},
                new Contact(){Name = "abinaya", PhoneNumber="8936740923"},
                new Contact(){Name = "abinaya", PhoneNumber="8936740923"},
                new Contact(){Name = "abinaya", PhoneNumber="8936740923"},
                new Contact(){Name = "abinaya", PhoneNumber="8936740923"},
                new Contact(){Name = "abinaya", PhoneNumber="8936740923"},
                new Contact(){Name = "abinaya", PhoneNumber="8936740923"},
                new Contact(){Name = "abinaya", PhoneNumber="8936740923"},
                new Contact(){Name = "abinaya", PhoneNumber="8936740923"},
                new Contact(){Name = "abinaya", PhoneNumber="8936740923"}
            };
        }

        public ContactBook(List<Contact> contacts)
        {
            Contacts = contacts;
        }
        #endregion
    }
}
