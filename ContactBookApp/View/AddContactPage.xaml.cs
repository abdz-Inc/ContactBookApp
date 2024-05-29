using ContactBookApp.ViewModel;

namespace ContactBookApp.View;

public partial class AddContactPage : ContentPage
{
    AddContactViewModel viewModel;
    public AddContactPage(AddContactViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        viewModel = vm;
    }
    protected override bool OnBackButtonPressed()
    {
        viewModel.ClearContact();
        return true;
    }
}