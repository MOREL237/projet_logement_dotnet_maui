using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetHome.Localization;
using GetHome.Models;
using GetHome.Services;

namespace GetHome.ViewModels;

public partial class AccueilViewModel : ObservableObject
{
    private readonly IDataService _dataService;

    public AccueilViewModel(IDataService dataService)
    {
        _dataService = dataService;
        LocalizationService.Instance.PropertyChanged += OnCultureChanged;
    }

    private void OnCultureChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        OnPropertyChanged(nameof(Greeting));
        OnPropertyChanged(nameof(AlertsText));
    }

    public ObservableCollection<SavedSearch> SavedSearches { get; } = new();
    public ObservableCollection<Listing> Listings { get; } = new();

    [ObservableProperty]
    private string userName = string.Empty;

    [ObservableProperty]
    private int activeAlerts;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    private bool isBusy;

    public bool IsNotBusy => !IsBusy;
    public string Greeting => string.Format(LocalizationService.Instance["Accueil_Greeting"], UserName);
    public string AlertsText => string.Format(LocalizationService.Instance["Accueil_Alerts"], ActiveAlerts);

    partial void OnUserNameChanged(string value) => OnPropertyChanged(nameof(Greeting));
    partial void OnActiveAlertsChanged(int value) => OnPropertyChanged(nameof(AlertsText));

    [RelayCommand]
    private async Task LoadAsync()
    {
        if (IsBusy)
            return;

        IsBusy = true;
        try
        {
            UserName = _dataService.CurrentUserName;
            ActiveAlerts = _dataService.ActiveAlerts;

            SavedSearches.Clear();
            foreach (var search in await _dataService.GetSavedSearchesAsync())
                SavedSearches.Add(search);

            Listings.Clear();
            foreach (var listing in await _dataService.GetRecentListingsAsync())
                Listings.Add(listing);
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task OpenListingAsync(Listing? listing)
    {
        if (listing is null)
            return;

        await Shell.Current.GoToAsync($"detail?id={listing.Id}");
    }

    [RelayCommand]
    private async Task ContactAsync(Listing? listing)
    {
        if (listing is null)
            return;

        await Shell.Current.GoToAsync("//main/messages");
    }

    [RelayCommand]
    private async Task OpenSearchAsync(SavedSearch? search)
    {
        if (search is null)
            return;

        await Shell.Current.GoToAsync("resultats");
    }

    [RelayCommand]
    private async Task FilterAsync() =>
        await Shell.Current.GoToAsync("filtres");
}
