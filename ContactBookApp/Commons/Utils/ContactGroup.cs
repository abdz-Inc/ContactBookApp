using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using ContactBookApp.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmHelpers;
using System.Runtime.CompilerServices;
using CommunityToolkit.Maui.Core.Extensions;

namespace ContactBookApp.Commons.Utils
{


    public partial class ContactGroup : ObservableRangeCollection<Model.Contact>
    {

        #region Field
        private string groupName;
        private bool isVisible;
        private ObservableRangeCollection<Model.Contact> hiddenContacts;
        #endregion

        #region Property
        public string GroupName { get; private set; }
        public bool IsVisible { get => isVisible; 
            set
            {
                isVisible = value;
                OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(nameof(IsVisible)));
            } }
        /// public ObservableCollection<Model.Contact> Contacts { get; set; }
        #endregion

        #region Constructor
        public ContactGroup(string groupName, IEnumerable<Model.Contact> contacts, bool isVisible = true) : base(contacts)
        {
            GroupName = groupName;
            IsVisible = isVisible;
            hiddenContacts = new();
        }

        #endregion

        #region Methods
        ///public int Count() => Contacts.Count;

        private void AddToHiddenContacts(Model.Contact contact)
        {
            if(hiddenContacts.Count == 0)
            {
                hiddenContacts.Add(contact);
                return;
            }
            int index = hiddenContacts.BinarySearch(contact.Name);
            hiddenContacts.Insert(index, contact);
        }

        private void AddToBase(Model.Contact contact)
        {
            if (base.Count == 0)
            {
                base.Add(contact);
                return;
            }
            int index = BinarySearchBase(contact.Name);
            base.Insert(index, contact);
        }

        public void AddContact(Model.Contact contact)
        {

            if (IsVisible) AddToBase(contact);

            else AddToHiddenContacts(contact);

        }

        public void RemoveContact(Model.Contact contact) 
        {
            if (IsVisible) RemoveFromBase(contact);

            else RemoveFromHiddenContacts(contact);
        }

        private void RemoveFromHiddenContacts(Model.Contact contact)
        {
            if (hiddenContacts.Count == 1)
            {
                hiddenContacts.RemoveAt(0);
                return;
            }
            int index = hiddenContacts.BinarySearch(contact.Name);
            hiddenContacts.RemoveAt(index);
        }

        private void RemoveFromBase(Model.Contact contact)
        {
            if (base.Count == 1)
            {
                base.RemoveAt(0);
                return;
            }
            int index = BinarySearchBase(contact.Name);
            base.RemoveAt(index);
        }

        public ContactGroup SearchContacts(string contactName)
        {
            if (IsVisible) return new ContactGroup(GroupName, base.Items.Where(x => x.Name.Contains(contactName, StringComparison.CurrentCultureIgnoreCase)).ToObservableCollection(), IsVisible);
            return new ContactGroup(GroupName, hiddenContacts.Where(x => x.Name.Contains(contactName, StringComparison.CurrentCultureIgnoreCase)).ToObservableCollection(), IsVisible);
        }
        public void ToggleVisibility()
        {
            if (base.Count == 0 && hiddenContacts.Count == 0) return;
            if (IsVisible)
            {
                hiddenContacts.AddRange(base.Items);
                base.RemoveRange(hiddenContacts);
                IsVisible = false;
            }
            else
            {
                base.AddRange(hiddenContacts);
                hiddenContacts.RemoveRange(base.Items);
                IsVisible = true;
            }

        }

        private int BinarySearchBase(string Name)
        {
            int min = 0;
            int max = base.Count - 1;
            while (min <= max)
            {
                int mid = (min + max) / 2;
                if (Name.Equals(base[mid].Name))
                {
                    return mid;
                }
                else if (Name.CompareTo(base[mid].Name) < 0)
                {
                    max = mid - 1;
                }
                else
                {
                    min = mid + 1;
                }

            }
            return min;

        }

        #endregion


    }
}
