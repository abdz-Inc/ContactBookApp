using ContactBookApp.View;
using ContactBookApp.ViewModel;

namespace ContactBookApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            /// Routing.RegisterRoute(nameof(AddContactPage), typeof(AddContactPage));
            Routing.RegisterRoute(nameof(AddContactAlternative), typeof(AddContactAlternative));
            Routing.RegisterRoute(nameof(EditContactPage), typeof(EditContactPage));
        }
    }
}
