using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContactBookApp.Commons.Utils;
using ContactBookApp.Model;
using Microsoft.Maui.ApplicationModel.Communication;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBookApp.ViewModel
{
    [ObservableObject]
    public partial class MainPageViewModel
    {
        #region Fields
        [ObservableProperty]
        private ObservableRangeCollection<ContactGroup> contacts;
        private List<Model.Contact> sortedContacts;
        private int currentPosition;
        private int previousPosition;
        /// private IEnumerable<Model.Contact> loadedItems;
        #endregion

        #region Properties
        public int RetrievedItemsThreshold { get; set; }
        #endregion

        #region Constructors
        public MainPageViewModel(ContactBook contactBook)
        {
            Contacts = new ObservableRangeCollection<ContactGroup>()
            {
                new ContactGroup("A", new List<Model.Contact>()),
                new ContactGroup("B", new List<Model.Contact>()),
                new ContactGroup("C", new List<Model.Contact>()),
                new ContactGroup("D", new List<Model.Contact>()),
                new ContactGroup("E", new List<Model.Contact>()),
                new ContactGroup("F", new List<Model.Contact>()),
                new ContactGroup("G", new List<Model.Contact>()),
                new ContactGroup("H", new List<Model.Contact>()),
                new ContactGroup("I", new List<Model.Contact>()),
                new ContactGroup("J", new List<Model.Contact>()),
                new ContactGroup("K", new List<Model.Contact>()),
                new ContactGroup("L", new List<Model.Contact>()),
                new ContactGroup("M", new List<Model.Contact>()),
                new ContactGroup("N", new List<Model.Contact>()),
                new ContactGroup("O", new List<Model.Contact>()),
                new ContactGroup("P", new List<Model.Contact>()),
                new ContactGroup("Q", new List<Model.Contact>()),
                new ContactGroup("R", new List<Model.Contact>()),
                new ContactGroup("S", new List<Model.Contact>()),
                new ContactGroup("T", new List<Model.Contact>()),
                new ContactGroup("U", new List<Model.Contact>()),
                new ContactGroup("V", new List<Model.Contact>()),
                new ContactGroup("W", new List<Model.Contact>()),
                new ContactGroup("X", new List<Model.Contact>()),
                new ContactGroup("Y", new List<Model.Contact>()),
                new ContactGroup("Z", new List<Model.Contact>()),
            };
            /// loadedItems = new List<Model.Contact>();    
            sortedContacts = contactBook.Contacts.OrderBy(c => c.Name).ToList();
            RetrievedItemsThreshold = 30;
            currentPosition = 0;
            Application.Current.RequestedThemeChanged += (s, e) => RefreshContacts();
            LoadMoreContacts();
        }
        #endregion

        [RelayCommand]
        public void LoadMoreContacts()
        {
            if (Contacts != null)
            {
                /*loadedItems = sortedContacts
                    .Skip(currentPosition)
                    .Take(RetrievedItemsThreshold);*/
                previousPosition = currentPosition;
                while (currentPosition < sortedContacts.Count() && currentPosition < previousPosition + RetrievedItemsThreshold)
                {
                    Contacts[char.ToLower(sortedContacts[currentPosition].Name[0]) - 'a'].AddContact(sortedContacts[currentPosition]);
                    currentPosition += 1;
                }

            }
        }

        [RelayCommand]
        public void ToggleFavourite(Model.Contact contact)
        {
            /// contact.IsFavourite = !contact.IsFavourite;
            if (contact != null)
            {
                /// OnPropertyChanging(nameof(Contacts));
                contact.IsFavourite = !contact.IsFavourite;
                OnPropertyChanged(nameof(Contacts));
            }
        }

        [RelayCommand]
        public async void DeleteContact(Model.Contact contact)
        {
            bool delete = await Application.Current.MainPage.DisplayAlert("Delete", "Do you want to delete this contact?", "Yes", "No");
            if (delete)
            {
                Contacts[char.ToLower(contact.Name[0]) - 'a'].Remove(contact);
                OnPropertyChanged(nameof(Contacts));
            }

        }

        [RelayCommand]
        public void ToggleGroup(ContactGroup group)
        {
            group.ToggleVisibility();
            OnPropertyChanged(nameof(Contacts));
        }

        public void RefreshContacts()
        {
            var tempContacts = Contacts.ToObservableCollection();
            Contacts.Clear();
            Contacts.AddRange(tempContacts);
        }

    }



}
