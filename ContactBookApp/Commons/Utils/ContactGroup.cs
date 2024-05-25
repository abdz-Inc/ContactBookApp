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


    public class ContactGroup : ObservableRangeCollection<Model.Contact>
    {

        #region Field
        private string groupName;
        private bool isVisible;
        private ObservableRangeCollection<Model.Contact> hiddenContacts;
        #endregion

        #region Property
        public string GroupName { get; private set; }
        /// public ObservableCollection<Model.Contact> Contacts { get; set; }
        public bool IsVisible { get; set; }
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
            int index = hiddenContacts.BinarySearch(contact);
            hiddenContacts.Insert(index, contact);
        }

        private void AddToBase(Model.Contact contact)
        {
            if (base.Count == 0)
            {
                base.Add(contact);
                return;
            }
            int index = BinarySearchBase(contact);
            base.Insert(index, contact);
        }

        public void AddContact(Model.Contact contact)
        {

            if (IsVisible) AddToBase(contact);

            else AddToHiddenContacts(contact);

        }
        
        public void ToggleVisibility()
        {
            if (base.Count == 0 && hiddenContacts.Count == 0) return;
            if (IsVisible)
            {
                for (int i = 0; i < base.Count; i++) hiddenContacts.Add(base.Items[i]);
                base.RemoveRange(hiddenContacts);
                IsVisible = false;
            }
            else
            {
                base.AddRange(hiddenContacts);
                hiddenContacts.Clear();
                IsVisible = true;
            }

        }

        private int BinarySearchBase(Model.Contact key)
        {
            int min = 0;
            int max = base.Count - 1;
            while (min <= max)
            {
                int mid = (min + max) / 2;
                if (key.Name.Equals(base[mid].Name))
                {
                    return ++mid;
                }
                else if (key.Name.CompareTo(base[mid].Name) < 0)
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
