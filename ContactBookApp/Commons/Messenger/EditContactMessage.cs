using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBookApp.Commons.Messenger
{
    class EditContactMessage : ValueChangedMessage<Model.Contact>
    {
        public EditContactMessage(Model.Contact value) : base(value)
        {

        }
    }
}
