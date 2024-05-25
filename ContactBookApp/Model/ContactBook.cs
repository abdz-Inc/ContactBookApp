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
                new Contact(){Name = "Abdz", PhoneNumber="2453679872"},
                new Contact(){Name = "Abinaya", PhoneNumber="8936740923"},
                new Contact(){Name = "Harish", PhoneNumber="8936740923"},
                new Contact(){Name = "Jeyaprakash", PhoneNumber="9356740923"},
                new Contact(){Name = "Avantika", PhoneNumber="8936740923"},
                new Contact(){Name = "Harsh", PhoneNumber="8036740923"},
                new Contact(){Name = "Hari", PhoneNumber="8936350923"},
                new Contact(){Name = "Deva", PhoneNumber="8936740923"},
                new Contact(){Name = "Abhishek", PhoneNumber="8936740923"},
                new Contact(){Name = "Abrar", PhoneNumber="9240204031"},
                new Contact(){Name = "Charan", PhoneNumber="8936740923"},
                new Contact(){Name = "Christy", PhoneNumber="8936740923"},
                new Contact(){Name = "Hannah", PhoneNumber="8936740923"},
                new Contact(){Name = "Srk", PhoneNumber="8936740923"},
                new Contact(){Name = "Rajini", PhoneNumber="8936740923"},
                new Contact(){Name = "Vijay", PhoneNumber="8936740923"},
                new Contact(){Name = "Dhanuush", PhoneNumber="8936740923"},
                new Contact(){Name = "Goki", PhoneNumber="8936740923"},
                new Contact(){Name = "Harani", PhoneNumber="9054060302"}
            };
        }

        public ContactBook(List<Contact> contacts)
        {
            Contacts = contacts;
        }
        #endregion
    }
}
