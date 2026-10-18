using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetHome.Localization;
using GetHome.Models;
using GetHome.Services;

namespace GetHome.ViewModels;

public partial class ResultatsViewModel : ObservableObject
{
    private readonly IDataService _dataService;

    public ResultatsViewModel(IDataService dataService)
    {
        _dataService = dataService;
        LocalizationService.Instance.PropertyChanged += (_, _) =>
        {
            OnPropertyChanged(nameof(SortText));
            OnPropertyChanged(nameof(FilterButtonText));
            OnPropertyChanged(nameof(ResultCountText));
        };
    }

    public ObservableCollection<Listing> Listings { get; } = new();

    [ObservableProperty]
    private int filterCount = 3;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    private bool isBusy;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsListView))]
    [NotifyPropertyChangedFor(nameof(IsMapView))]
    private bool showMap;

    public bool IsNotBusy => !IsBusy;
    public bool IsListView => !ShowMap;
    public bool IsMapView => ShowMap;

    public string ResultCountText => string.Format(LocalizationService.Instance["Resultats_CountText"], Listings.Count);
    public string SortText => LocalizationService.Instance["Resultats_Sort"];
    public string FilterButtonText => string.Format(LocalizationService.Instance["Resultats_Filters"], FilterCount);

    partial void OnFilterCountChanged(int value) => OnPropertyChanged(nameof(FilterButtonText));

    [RelayCommand]
    private async Task LoadAsync()
    {
        if (IsBusy)
            return;

        IsBusy = true;
        try
        {
            Listings.Clear();
            foreach (var listing in await _dataService.GetListingsAsync())
                Listings.Add(listing);

            OnPropertyChanged(nameof(ResultCountText));
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand] private void ShowList() => ShowMap = false;
    [RelayCommand] private void ShowMapView() => ShowMap = true;

    [RelayCommand]
    private async Task OpenFilterAsync() => await Shell.Current.GoToAsync("filtres");

    [RelayCommand]
    private async Task OpenDetailAsync(Listing? listing)
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
    private async Task LoadMoreAsync() =>
        await Shell.Current.DisplayAlert("GetHome", LocalizationService.Instance["Resultats_LoadMoreSoon"], "OK");

    [RelayCommand]
    private async Task GoBackAsync() => await Shell.Current.GoToAsync("..");
}
