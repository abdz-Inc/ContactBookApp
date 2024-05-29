using ContactBookApp.Commons.Utils;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBookApp.Commons.Services
{
    public class ContactBookService
    {
        private List<Model.Contact> sortedContacts;
        private List<ContactGroup> contactGroupList;
        private int currentPosition;
        private int previousPosition;

        public List<Model.Contact> SortedContacts { get; set; }
        public List<ContactGroup> ContactGroupList { get; set; }
        public int RetrievedItemsThreshold { get; set; }

        public ContactBookService(Model.ContactBook contactBook)
        {
            SortedContacts = contactBook.Contacts.OrderBy(c => c.Name).ToList();
            ReloadContacts();
        }


        private void InitializeContacts() => ContactGroupList = new List<ContactGroup>()
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


        public void ReloadContacts()
        {
            ContactGroupList?.Clear();
            InitializeContacts();
            currentPosition = 0;
            LoadMoreContacts();
        }

        public void LoadMoreContacts()
        {
            if (ContactGroupList != null)
            {
                /*loadedItems = sortedContacts
                    .Skip(currentPosition)
                    .Take(RetrievedItemsThreshold);*/
                previousPosition = currentPosition;
                while (currentPosition < sortedContacts.Count() && currentPosition < previousPosition + RetrievedItemsThreshold)
                {
                    ContactGroupList[char.ToLower(sortedContacts[currentPosition].Name[0]) - 'a'].AddContact(sortedContacts[currentPosition]);
                    currentPosition += 1;
                }

            }
        }
    }
}
