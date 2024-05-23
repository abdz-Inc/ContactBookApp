using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ContactBookApp.Model
{
    public class Contact
    {

        #region Fields
        private string name;
        private string phoneNumber;
        private string profilePic;
        #endregion


        #region Properties
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string ProfilePic { get; set; }

        public bool Isfavourite { get; set; }
        #endregion

        #region Constructors
        public Contact() 
        {
            ProfilePic = null;
            Isfavourite = false;
        }

        public Contact(string name, string phoneNumber, string profilePic, bool isfavourite)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            ProfilePic = profilePic;
            Isfavourite = isfavourite;
        }
        #endregion
    }
}
