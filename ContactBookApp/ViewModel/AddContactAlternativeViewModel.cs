using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ContactBookApp.Commons.Messenger;
using ContactBookApp.Commons.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBookApp.ViewModel
{
    [ObservableObject]
    public partial class AddContactAlternativeViewModel
    {

        #region Fields
        [ObservableProperty]
        private Model.Contact contact;

        [ObservableProperty]
        public bool isFavourite;

        [ObservableProperty]
        public ContactValidationRule validator;
        #endregion

        #region Constructor
        public AddContactAlternativeViewModel(Model.Contact contact)
        {
            /// Instantiate Validator object with new contact instance
            Contact = contact;
            Validator = new ContactValidationRule() { Contact = Contact};

            /// Handle Apptheme Change
            Application.Current!.RequestedThemeChanged += (s, e) => OnPropertyChanged("Refresh");
        }
        #endregion

        #region Commands
        /// <summary>
        /// Validate and Send AddContactMessage to recievers.
        /// </summary>
        [RelayCommand]
        public async Task AddContactAsync()
        {

            Validator.Validate();
            if (Validator.IsValid)
            {
                WeakReferenceMessenger.Default.Send(new AddContactMessage(Contact));
                await Application.Current!.MainPage!.DisplayAlert("Add Contact", "Contact Added Successfully", "Great!");
                ClearContact();
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Application.Current!.MainPage!.DisplayAlert("Oops!", "Enter a Valid Contact", "Ok");
            }

        }


        /// <summary>
        /// Reinitialize Contact object to clear existing data.
        /// </summary>
        [RelayCommand]
        public void ClearContact()
        {

            Contact = new();
            Validator.Contact = Contact;

        }


        /// <summary>
        /// Toggle favourite property and update UI.
        /// </summary>
        [RelayCommand]
        public void ToggleFavourite()
        {
            
            if (Contact != null)
            {

                IsFavourite = !IsFavourite;
                Contact.IsFavourite = IsFavourite;
                OnPropertyChanged(nameof(Contact));

            }

        }       
        #endregion

    }
}
