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
        [ObservableProperty]
        private Model.Contact contact;

        [ObservableProperty]
        public bool isFavourite;

        public ContactValidationRule Validator { get; private set; }

        public AddContactAlternativeViewModel(Model.Contact contact)
        {
            Contact = contact;
            Validator = new ContactValidationRule() { Contact = Contact};
            Application.Current.RequestedThemeChanged += (s, e) => OnPropertyChanged("Refresh");
        }

        [RelayCommand]
        public void ToggleFavourite()
        {
            /// contact.IsFavourite = !contact.IsFavourite;
            if (Contact != null)
            {
                /// OnPropertyChanging(nameof(Contacts));
                IsFavourite = !IsFavourite;
                Contact.IsFavourite = IsFavourite;
                OnPropertyChanged(nameof(Contact));
            }
        }

        [RelayCommand]
        public async Task AddContactAsync()
        {
            /// contactBook.Contacts.Add(Contact);
            /// OnContactAdded(Contact);
            Validator.Validate();
            if (Validator.IsValid)
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
        public void ClearContact()
        {
            Contact = new();
            Validator.Contact = Contact;
        }
    }
}
