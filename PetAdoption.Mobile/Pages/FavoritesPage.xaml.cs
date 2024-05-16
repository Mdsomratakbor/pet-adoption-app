namespace PetAdoption.Mobile.Pages;

public partial class FavoritesPage : ContentPage
{
    private readonly FavoritesViewModel _viewModel;

    public FavoritesPage(FavoritesViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
 
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.InitializeAsync();
        BindingContext = _viewModel;
    }
}