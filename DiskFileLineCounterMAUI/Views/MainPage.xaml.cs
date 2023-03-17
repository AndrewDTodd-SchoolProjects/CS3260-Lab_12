using DiskFileLineCounterMAUI.ViewModels;

namespace DiskFileLineCounterMAUI.Views;

public partial class MainPage : ContentPage
{
    #region CONSTRUCTORS
    public MainPage(MainPageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
	}
    #endregion
}

