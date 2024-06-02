using ContactBookApp.ViewModel;

namespace ContactBookApp
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel viewModel;
        public MainPage(MainPageViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
            viewModel = vm;
        }
    }

}