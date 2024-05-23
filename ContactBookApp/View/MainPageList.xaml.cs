
using ContactBookApp.ViewModel;

namespace ContactBookApp.View;


public partial class MainPageList
{
	public MainPageList(MainPageViewModel vm) 
	{
		InitializeComponent();
		BindingContext = vm;
	}
}