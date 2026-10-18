using GetHome.ViewModels;

namespace GetHome.Views;

public partial class PublierPage : ContentPage
{
    public PublierPage(PublierViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
