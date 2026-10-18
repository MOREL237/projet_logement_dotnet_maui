using GetHome.ViewModels;

namespace GetHome.Views;

public partial class FavorisPage : ContentPage
{
    private readonly FavorisViewModel _viewModel;

    public FavorisPage(FavorisViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadCommand.Execute(null);
    }
}
