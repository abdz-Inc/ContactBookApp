namespace ContactBookApp.ViewModel;

public partial class AddContactAlternative : ContentPage
{
	public AddContactAlternative(AddContactAlternativeViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}