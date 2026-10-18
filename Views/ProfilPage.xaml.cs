using GetHome.ViewModels;

namespace GetHome.Views;

public partial class ProfilPage : ContentPage
{
    private readonly ProfilViewModel _viewModel;

    public ProfilPage(ProfilViewModel viewModel)
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
