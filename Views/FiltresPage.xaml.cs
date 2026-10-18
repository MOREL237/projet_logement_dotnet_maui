using GetHome.ViewModels;

namespace GetHome.Views;

public partial class FiltresPage : ContentPage
{
    public FiltresPage(FiltresViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
