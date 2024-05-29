using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContactBookApp.Commons.Utils;
using ContactBookApp.ViewModel;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBookApp.Commons.Handlers
{
    public partial class MainPageSearchHandler : SearchHandler
    {
        private ObservableCollection<ContactGroup> temp = new();

        private ObservableRangeCollection<ContactGroup> contacts;

        public ObservableRangeCollection<ContactGroup> Contacts { get => contacts; set => contacts = value; }

        /// public MainPageViewModel vm { get; set; }
        /// public ObservableRangeCollection<ContactGroup> Contacts {  get; set; }

        public MainPageSearchHandler()
        {
            /// this.vm = vm;
        }

        [RelayCommand]
        public async void SearchContact(string contactName)
        {
            if (temp.Count == 0) temp = Contacts.ToObservableCollection();
            Contacts.Clear();
            foreach (var group in temp) Contacts.Add(group.SearchContacts(contactName));
        }

        [RelayCommand]
        public async void ClearSearch()
        {
            Contacts.AddRange(temp);
            temp.Clear();
        }
    }
}
