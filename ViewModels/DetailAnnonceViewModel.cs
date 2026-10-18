using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetHome.Localization;
using GetHome.Models;
using GetHome.Services;

namespace GetHome.ViewModels;

public partial class DetailAnnonceViewModel : ObservableObject, IQueryAttributable
{
    private readonly IDataService _dataService;

    public DetailAnnonceViewModel(IDataService dataService)
    {
        _dataService = dataService;
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasListing))]
    private Listing? listing;

    [ObservableProperty]
    private bool isFavorite;

    [ObservableProperty]
    private string favoriteIcon = "♡";

    public bool HasListing => Listing is not null;

    partial void OnIsFavoriteChanged(bool value) => FavoriteIcon = value ? "♥" : "♡";

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("id", out var raw) && int.TryParse(raw?.ToString(), out var id))
            _ = LoadAsync(id);
    }

    private async Task LoadAsync(int id)
    {
        Listing = await _dataService.GetListingAsync(id);
    }

    [RelayCommand]
    private void ToggleFavorite() => IsFavorite = !IsFavorite;

    [RelayCommand]
    private async Task ContactAsync()
    {
        if (Listing is null)
            return;

        await Shell.Current.GoToAsync("//main/messages");
    }

    [RelayCommand]
    private async Task CallAsync()
    {
        if (Listing is null)
            return;

        try
        {
            // OwnerPhone not present on Listing model; keep placeholder fallback.
            await Launcher.OpenAsync(new Uri("tel:0000000000"));
        }
        catch
        {
            await Shell.Current.DisplayAlert("GetHome",
                string.Format(LocalizationService.Instance["Detail_CallSoon"], Listing.OwnerName), "OK");
        }
    }

    [RelayCommand]
    private async Task PlanVisitAsync() =>
        await Shell.Current.DisplayAlert("GetHome", LocalizationService.Instance["Detail_VisitSoon"], "OK");

    [RelayCommand]
    private async Task GoBackAsync() => await Shell.Current.GoToAsync("..");
}
