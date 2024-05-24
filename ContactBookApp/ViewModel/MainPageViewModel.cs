
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContactBookApp.Model;
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
        private ObservableRangeCollection<Model.Contact> contacts;
        private ContactBook contactBook;
        private int currentPosition;
        #endregion

        #region Properties
        public ObservableRangeCollection <Model.Contact> Contacts
        {
            get; set;
        }

        public int RetrievedItemsThreshold { get; set; }
        #endregion

        #region Constructors
        public MainPageViewModel(ContactBook contactBook) 
        {
            Contacts = new ObservableRangeCollection<Model.Contact>();
            this.contactBook = contactBook;
            RetrievedItemsThreshold = 10;
            currentPosition = 0;
            LoadMoreContacts();
        }
        #endregion

        [RelayCommand]
        public void LoadMoreContacts()
        {
            if(Contacts != null)
            {
                var loadeditems = contactBook.Contacts.Skip(currentPosition).Take(RetrievedItemsThreshold);
                Contacts.AddRange(loadeditems);
                currentPosition += loadeditems.Count();
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


    }
}
