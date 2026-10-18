using GetHome.ViewModels;

namespace GetHome.Views;

public partial class ResultatsPage : ContentPage
{
    private readonly ResultatsViewModel _viewModel;

    public ResultatsPage(ResultatsViewModel viewModel)
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
