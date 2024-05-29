using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ContactBookApp.Commons.Messenger;
using ContactBookApp.Commons.Utils;
using ContactBookApp.Commons.Validation;
using ContactBookApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBookApp.ViewModel
{
    [ObservableObject]
    public partial class AddContactViewModel
    {

        private ContactBook contactBook;
        private ContactValidationRule validator;

        [ObservableProperty]
        private Model.Contact contact;

        public event EventHandler<AddContactEventArgs> ContactAdded;

        public AddContactViewModel(ContactBook contactBook)
        {
            /*this.contactBook = contactBook;
            Contact = new Model.Contact("","",null,false);
            validator = new ContactValidationRule() { Contact = Contact};
            /// AttachValidationRules();
            Application.Current.RequestedThemeChanged += (s, e) => OnPropertyChanged("Refresh");*/
        }

        /// private void AttachValidationRules()
        /// {
        ///    Contact.Validations.Add(new ContactNameValidationRule<Model.Contact>());
        ///    Contact.Validations.Add(new ContactPhoneNumberValidationRule<Model.Contact>());
        /// }

        [RelayCommand]
        public void TriggerValidation()
        {
            validator.Validate();
        }

        [RelayCommand]                    
        public async Task AddContactAsync()
        {
            contactBook.Contacts.Add(Contact);
            /// OnContactAdded(Contact);
            validator.Validate();
            if (validator.IsValid)
            {
                WeakReferenceMessenger.Default.Send(new AddContactMessage(Contact));
                await Application.Current.MainPage.DisplayAlert("Add Contact", "Contact Added Successfully", "Great!");
                ClearContact();
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Oops!", "Enter a Valid Contact", "Ok");
            }
        }

        [RelayCommand]
        public void ToggleFavourite()
        {
            /// contact.IsFavourite = !contact.IsFavourite;
            if (Contact != null)
            {
                /// OnPropertyChanging(nameof(Contacts));
                Contact.IsFavourite = !Contact.IsFavourite;
                OnPropertyChanged(nameof(Contact));
            }
        }

        [RelayCommand]
        public void ClearContact()
        {
            Contact = new();
            validator.Contact = Contact;
        }

        protected void OnContactAdded(Model.Contact contact) => ContactAdded?.Invoke(this, new AddContactEventArgs(contact));

    }
}
