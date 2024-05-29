using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBookApp.Commons.Utils
{
    public class AddContactEventArgs : EventArgs
    {
        private Model.Contact addedContact;

        public Model.Contact AddedContact { get; set; }

        public AddContactEventArgs(Model.Contact contact) => AddedContact = contact;
    }
}
