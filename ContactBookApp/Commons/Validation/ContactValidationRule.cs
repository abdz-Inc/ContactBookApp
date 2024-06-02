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

        #region Fields
        private Model.Contact contact;

        [ObservableProperty]
        public bool isNameValid;

        [ObservableProperty]
        public bool isPhoneNumberValid;

        private Regex pattern = new Regex("^[0-9]{10}$");
        #endregion

        #region Property
        public Model.Contact Contact 
        { 
            get => contact; 
            set => contact = value; 
        }

        public bool IsValid
        {
            get => IsNameValid && IsPhoneNumberValid;
        }
        #endregion

        #region Constructor
        public ContactValidationRule()
        {
            IsNameValid = false;
            IsPhoneNumberValid = false;
        }
        #endregion

        #region Methods
        /// <summary>
        /// validate name of the contact object.
        /// </summary>
        /// <returns>
        /// true if name is valid.
        /// </returns>
        public bool CheckName()
        {

            IsNameValid = !string.IsNullOrEmpty(Contact.Name);
            return IsNameValid;
        
        }

        /// <summary>
        /// validate phonenumber of the contact object.
        /// </summary>
        /// <returns>
        /// true if phonenumber is valid.
        /// </returns>
        public bool CheckPhoneNumber()
        {
            if (string.IsNullOrEmpty(Contact.PhoneNumber) || Contact.PhoneNumber.Length != 10)
            {
                IsPhoneNumberValid = false;
                return false;
            }
            IsPhoneNumberValid = pattern.IsMatch(Contact.PhoneNumber);
            return IsPhoneNumberValid;
        }


        /// <summary>
        /// validate contact object.
        /// </summary>
        /// <returns>
        /// true if name and phonenumber property are valid.
        /// </returns>
        public bool Validate()
        {
            CheckName();
            CheckPhoneNumber();
            return IsValid;
        }
        #endregion
    }
}
