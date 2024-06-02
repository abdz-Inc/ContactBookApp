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
    [QueryProperty(nameof(Contact), nameof(Contact))]
    public partial class EditContactViewModel 
    {

        #region Fields
        public Model.Contact contact;

        private Model.Contact editableContact;

        [ObservableProperty]
        public bool isEditModeOn;

        [ObservableProperty]
        public bool isFavourite;

        [ObservableProperty]
        public ContactValidationRule validator;
        #endregion

        #region Properties
        public Model.Contact Contact
        {
            get
            {
                return contact;
            }
            set
            {
                EditableContact = value;
                IsFavourite = value.IsFavourite;
                SetProperty(ref contact, value);
            }
        }

        public Model.Contact EditableContact 
        {
            get
            {
                return editableContact;
            }
            set => SetProperty(ref editableContact, value);
        }
        #endregion

        #region Constructor
        public EditContactViewModel()
        {
            /// Instantiate Validator object
            Validator = new ContactValidationRule();

            /// Handle Apptheme Change
            Application.Current!.RequestedThemeChanged += (s, e) => OnPropertyChanged("Refresh");

        }
        #endregion

        #region Commands
        /// <summary>
        /// Reinitialize Contact and EditableContact object to clear existing data.
        /// </summary>
        [RelayCommand]
        public void ClearContact()
        {
            Contact = new();
            EditableContact = new();
        }


        /// <summary>
        /// Validate and Send EditContactMessage to recievers.
        /// </summary>
        [RelayCommand]
        public async Task EditContactAsync()
        {
            /// Attach EditabeContact to validator
            Validator.Contact = EditableContact;

            /// Validation
            Validator.Validate();
            if (Validator.IsValid)
            {
                WeakReferenceMessenger.Default.Send(new EditContactMessage(EditableContact));
                await Application.Current!.MainPage!.DisplayAlert("Edit Contact", "Contact Edited Successfully", "Great!");
                ClearContact();
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Application.Current!.MainPage!.DisplayAlert("Oops!", "Enter Valid Data", "Ok");
            }
        }


        /// <summary>
        /// Toggle edit contact menu.
        /// </summary>
        [RelayCommand]
        public void ToggleEdit()
        {
            
            if (Contact != null)
            {

                IsEditModeOn = !IsEditModeOn;

                if (IsEditModeOn) 
                    EditableContact = new(Contact.Name, Contact.PhoneNumber, Contact.ProfilePic, Contact.IsFavourite);
                else 
                    EditableContact = Contact;

                OnPropertyChanged(nameof(IsEditModeOn));

            }

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