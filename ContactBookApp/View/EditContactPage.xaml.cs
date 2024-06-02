using ContactBookApp.ViewModel;

namespace ContactBookApp.View;

public partial class EditContactPage : ContentPage
{
	public EditContactPage(EditContactViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

	protected override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);
	}
}