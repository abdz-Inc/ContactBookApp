using ContactBookApp.ViewModel;

namespace ContactBookApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

        private void search_Completed(object sender, EventArgs e)
        {

        }

        
    }

}
