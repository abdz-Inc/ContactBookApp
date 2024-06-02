using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ContactBookApp.Commons.Messenger;
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

    public partial class MainPageViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject, IRecipient<AddContactMessage>, IRecipient<EditContactMessage>
    {
        #region Fields
        private ContactBook contactBook;

        [ObservableProperty]
        private ObservableRangeCollection<ContactGroup> contacts;

        private int currentPosition;
        private int previousPosition;
        private List<Model.Contact> sortedContacts;
        private ObservableCollection<ContactGroup> temp = [];
        #endregion

        #region Properties
        public int RetrievedItemsThreshold { get; set; }

        #endregion

        #region Constructors
        public MainPageViewModel(ContactBook contactBook)
        {

            /// Variable Initialization
            this.contactBook = contactBook;
            RetrievedItemsThreshold = 30;

            /// Register to Messengers
            WeakReferenceMessenger.Default.Register<AddContactMessage>(this);
            WeakReferenceMessenger.Default.Register<EditContactMessage>(this);

            /// Handle Apptheme Change
            Application.Current!.RequestedThemeChanged += (s, e) => RefreshContacts();

            /// Generate GroupedContactList
            ReloadContacts();

        }
        #endregion

        #region Commands

        /// <summary>
        /// Delete the contact from database.
        /// </summary>
        /// <param name="contact">
        /// Contact reference to delete.
        /// </param>
        [RelayCommand]
        public async Task DeleteContact(Model.Contact contact)
        {
            bool delete = await Application.Current!.MainPage!
                .DisplayAlert("Delete", "Do you want to delete this contact?", "Yes", "No");
            if (delete)
            {
                Contacts[char.ToLower(contact.Name[0]) - 'a'].Remove(contact);
                OnPropertyChanged(nameof(Contacts));
            }
        }

        /// <summary>
        /// Navigate to AddContact Page.
        /// </summary>
        [RelayCommand]
        public async Task GoToAddContactPage() =>

            await Shell.Current.GoToAsync("AddContactAlternative");


        /// <summary>
        /// Go to Edit Contact Page.
        /// </summary>
        /// <param name="contact">
        /// Contact reference to edit.
        /// </param>
        [RelayCommand]
        public async Task GoToEditContactPage(Model.Contact contact) =>

            await Shell.Current.GoToAsync("EditContactPage", new Dictionary<string, Object>
            {
                ["Contact"] = contact
            });


        /// <summary>
        /// Buffer next batch of contacts.
        /// </summary>
        [RelayCommand]
        public void LoadMoreContacts()
        {

            if (Contacts != null)
            {

                previousPosition = currentPosition;
                while (currentPosition < sortedContacts.Count()
                    && currentPosition < previousPosition + RetrievedItemsThreshold)
                {

                    Contacts[char.ToLower(sortedContacts[currentPosition].Name[0]) - 'a']
                        .AddContact(sortedContacts[currentPosition]);

                    currentPosition += 1;

                }

            }

        }


        /// <summary>
        /// Search the contact based on name.
        /// </summary>
        /// <param name="contactName">
        /// Contact name to search.
        /// </param>
        [RelayCommand]
        public void SearchContact(string contactName)
        {

            if (temp.Count == 0) temp = Contacts.ToObservableCollection();
            Contacts.Clear();
            foreach (var group in temp) Contacts.Add(group.SearchContacts(contactName));

        }


        /// <summary>
        /// Toggle Favourite property of contact.
        /// </summary>
        /// <param name="contact">
        /// Reference of contact to toggle favourite.
        /// </param>
        [RelayCommand]
        public void ToggleFavourite(Model.Contact contact)
        {

            if (contact != null)
            {

                contact.IsFavourite = !contact.IsFavourite;
                OnPropertyChanged(nameof(Contacts));

            }

        }


        /// <summary>
        /// Expand/Collapse contact group.
        /// </summary>
        /// <param name="group">
        /// Name of group to toggle visibility.
        /// </param>
        [RelayCommand]
        public void ToggleGroupVisibility(ContactGroup group)
        {

            group.ToggleVisibility();
            OnPropertyChanged(nameof(Contacts));

        }
        #endregion

        #region Helpers

        /// <summary>
        /// Initialize grouped contact object.
        /// </summary>
        private void InitializeContacts() => Contacts = new ObservableRangeCollection<ContactGroup>()
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


        /// <summary>
        /// Refresh grouped contact list to trigger UI changes.
        /// </summary>
        public void RefreshContacts()
        {

            temp = Contacts.ToObservableCollection();
            Contacts.Clear();
            Contacts.AddRange(temp);
            temp.Clear();
            OnPropertyChanged(nameof(Contacts));

        }


        /// <summary>
        /// Reload data in grouped contact list from datasource.
        /// </summary>
        public void ReloadContacts()
        {

            Contacts?.Clear();
            InitializeContacts();
            sortedContacts = contactBook.Contacts.OrderBy(c => c.Name).ToList();
            currentPosition = 0;
            LoadMoreContacts();

        }
        #endregion

        #region MessageHandlers


        /// <summary>
        /// Add contact to GroupedContact List when AddContactMessage is sent.
        /// </summary>
        /// <param name="message">
        /// AddContactMessage Object with contact object to be added to GroupedContact List.
        /// </param>
        void IRecipient<AddContactMessage>.Receive(AddContactMessage message)
        {
            Contacts[char.ToLower(message.Value.Name[0]) - 'a'].AddContact(message.Value);
        }


        /// <summary>
        /// Edit contact from GroupedContact List when EditContactMessage is sent.
        /// </summary>
        /// <param name="message">
        /// EditContactMessage Object with contact object to be edited on GroupedContact List.
        /// </param>
        void IRecipient<EditContactMessage>.Receive(EditContactMessage message)
        {
            Contacts[char.ToLower(message.Value.Name[0]) - 'a'].EditContact(message.Value);
        }
        #endregion
    }






}
