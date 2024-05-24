using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ContactBookApp.Model
{
    [ObservableObject]
    public partial class Contact
    {

        #region Fields
        private string name;
        private string phoneNumber;
        private string profilePic;
        [ObservableProperty]
        private bool isFavourite;
        #endregion


        #region Properties
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string ProfilePic { get; set; }
        #endregion

        #region Constructors
        public Contact() 
        {
            ProfilePic = null;
            IsFavourite = false;
        }

        public Contact(string name, string phoneNumber, string profilePic, bool isfavourite)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            ProfilePic = profilePic;
            IsFavourite = isfavourite;
        }
        #endregion
    }
}
