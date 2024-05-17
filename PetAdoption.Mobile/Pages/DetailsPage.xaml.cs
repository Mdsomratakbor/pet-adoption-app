
namespace PetAdoption.Mobile.Pages;

public partial class DetailsPage : ContentPage, IAsyncDisposable
{
    private readonly DetailsViewModel _viewModel;

    public DetailsPage(DetailsViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    public async ValueTask DisposeAsync() => await _viewModel.StopHubConnection();

    protected override async void OnDisappearing()
    {
        base.OnDisappearing();
       await _viewModel.StopHubConnection();
    }
}