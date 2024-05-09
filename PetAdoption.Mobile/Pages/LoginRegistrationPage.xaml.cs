namespace PetAdoption.Mobile.Pages;

public partial class LoginRegistrationPage : ContentPage
{
    private readonly LoginRegisterViewModel _viewModel;

    public LoginRegistrationPage(LoginRegisterViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }


}