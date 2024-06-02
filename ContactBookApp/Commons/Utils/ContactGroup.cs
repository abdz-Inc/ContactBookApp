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
        private ObservableRangeCollection<Model.Contact> hiddenContacts;
        private bool isVisible;
        #endregion

        #region Property
        public string GroupName { get; private set; }
        public bool IsVisible { get => isVisible; 
            set
            {
                isVisible = value;
                OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(nameof(IsVisible)));
            } 
        }
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
        /// <summary>
        /// Add Contact to ContactGroup based on group visibility.
        /// </summary>
        /// <param name="contact">
        /// Contact object to be added.
        /// </param>
        public void AddContact(Model.Contact contact)
        {

            if (IsVisible) AddToBase(contact);

            else AddToHiddenContacts(contact);

        }

        /// <summary>
        /// Add contact to base class Collection.
        /// </summary>
        /// <param name="contact">
        /// Contact object to be added.
        /// </param>
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


        /// <summary>
        /// Add contact to hiddenContacts class Collection.
        /// </summary>
        /// <param name="contact">
        /// Contact object to be added.
        /// </param>
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


        /// <summary>
        /// Use binary search to find the index to insert given name in sorted list.
        /// </summary>
        /// <param name="Name">
        /// Name of contact to be added.
        /// </param>
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


        /// <summary>
        /// Edit Contact to ContactGroup based on group visibility.
        /// </summary>
        /// <param name="contact">
        /// Contact reference to be edited.
        /// </param>
        public void EditContact(Model.Contact contact)
        {

            if (IsVisible) EditInBase(contact);

            else EditInHiddenContacts(contact);

        }


        /// <summary>
        /// Edit contact in base class Collection.
        /// </summary>
        /// <param name="contact">
        /// Contact object to be edited.
        /// </param>
        private void EditInBase(Model.Contact contact)
        {
            int index = BinarySearchBase(contact.Name);
            base[index] = contact;
        }


        /// <summary>
        /// Edit contact in hiddenContacts class Collection.
        /// </summary>
        /// <param name="contact">
        /// Contact object to be edited.
        /// </param>
        private void EditInHiddenContacts(Model.Contact contact)
        {
            int index = hiddenContacts.BinarySearch(contact.Name);
            hiddenContacts[index] = contact;
        }


        /// <summary>
        /// Remove Contact from ContactGroup based on group visibility.
        /// </summary>
        /// <param name="contact">
        /// Contact reference to be removed.
        /// </param>
        public void RemoveContact(Model.Contact contact) 
        {
            if (IsVisible) RemoveFromBase(contact);

            else RemoveFromHiddenContacts(contact);
        }


        /// <summary>
        /// Remove contact in base class Collection.
        /// </summary>
        /// <param name="contact">
        /// Contact object to be removed.
        /// </param>
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


        /// <summary>
        /// Remove contact in hiddenContact class Collection.
        /// </summary>
        /// <param name="contact">
        /// Contact object to be removed.
        /// </param>
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


        /// <summary>
        /// Search Contact in ContactGroup based on group visibility.
        /// </summary>
        /// <param name="contact">
        /// ContactName to be searched.
        /// </param>
        public ContactGroup SearchContacts(string contactName)
        {
            if (IsVisible) return new ContactGroup(GroupName, base.Items.Where(x => x.Name.Contains(contactName, StringComparison.CurrentCultureIgnoreCase)).ToObservableCollection(), IsVisible);
            return new ContactGroup(GroupName, hiddenContacts.Where(x => x.Name.Contains(contactName, StringComparison.CurrentCultureIgnoreCase)).ToObservableCollection(), IsVisible);
        }


        /// <summary>
        /// Toggle group visibility
        /// </summary>
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
        #endregion


    }
}
