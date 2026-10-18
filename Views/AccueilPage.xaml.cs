using GetHome.ViewModels;

namespace GetHome.Views;

public partial class AccueilPage : ContentPage
{
    private readonly AccueilViewModel viewModel;

    public AccueilPage(AccueilViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = this.viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        viewModel.LoadCommand.Execute(null);
    }
}
