using CommunityToolkit.Mvvm.ComponentModel;

namespace GetHome.Models;

public partial class SelectOption : ObservableObject
{
    public SelectOption(string label) => Label = label;

    [ObservableProperty]
    private string label = string.Empty;

    [ObservableProperty]
    private bool isSelected;
}
