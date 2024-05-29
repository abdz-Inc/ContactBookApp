using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBookApp.Commons.Messenger
{
    public class AddContactMessage : ValueChangedMessage<Model.Contact>
    {
        public AddContactMessage(Model.Contact value) : base(value)
        {
        }

    }
}
