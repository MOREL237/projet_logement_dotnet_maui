using GetHome.ViewModels;

namespace GetHome.Views;

public partial class DetailAnnoncePage : ContentPage
{
    public DetailAnnoncePage(DetailAnnonceViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
