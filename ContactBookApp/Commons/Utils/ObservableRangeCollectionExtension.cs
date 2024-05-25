using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBookApp.Commons.Utils
{
    public static class ObservableCollectionExtension
    {
        /// <summary>
        /// Search the insertion index for a element in alphabetical order in O(logn) complexity for ObservableRangeCollection
        /// </summary>
        /// <param name="contacts"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int BinarySearch(this ObservableCollection<Model.Contact> contacts, Model.Contact key)
        {
            int min = 0;
            int max = contacts.Count() - 1;
            while (min <= max)
            {
                int mid = (min + max) / 2;
                if (key.Name.Equals(contacts[mid].Name))
                {
                    return ++mid;
                }
                else if (key.Name.CompareTo(contacts[mid].Name) < 0)
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
    }
}
