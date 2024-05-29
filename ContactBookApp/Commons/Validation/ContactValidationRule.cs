using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace ContactBookApp.Commons.Validation
{
    [ObservableObject]
    public partial class ContactValidationRule
    {
        private Regex pattern = new Regex("^[0-9]{10}$");

        private Model.Contact contact;

        [ObservableProperty]
        public bool isNameValid;

        [ObservableProperty]
        public bool isPhoneNumberValid;

        public Model.Contact Contact { get; set; }

        public ContactValidationRule()
        {
            IsNameValid = false;
            IsPhoneNumberValid = false;
        }

        public bool IsValid { 
            get =>  IsNameValid && IsPhoneNumberValid;     
        }

        public bool CheckName()
        {
            IsNameValid = !string.IsNullOrEmpty(Contact.Name);
            /// isValid = IsNameValid && IsPhoneNumberValid;
            return IsNameValid;
        }

        public bool CheckPhoneNumber()
        {
            if (string.IsNullOrEmpty(Contact.PhoneNumber) || Contact.PhoneNumber.Length != 10)
            {
                IsPhoneNumberValid = false;
                return false;
            }
            IsPhoneNumberValid = pattern.IsMatch(Contact.PhoneNumber);
            /// isValid = IsNameValid && IsPhoneNumberValid;
            return IsPhoneNumberValid;
        }

        public bool Validate()
        {
            CheckName();
            CheckPhoneNumber();
            return IsValid;
        }
    }
}
