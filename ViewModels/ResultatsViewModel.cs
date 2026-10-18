using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetHome.Models;
using GetHome.Services;

namespace GetHome.ViewModels;

public partial class ResultatsViewModel : ObservableObject
{
    private readonly IDataService _dataService;

    public ResultatsViewModel(IDataService dataService)
    {
        _dataService = dataService;
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

    public string ResultCountText => $"{Listings.Count} annonces vérifiées · Zéro intermédiaire";
    public string SortText => "Tri : Plus récents";
    public string FilterButtonText => $"Filtres · {FilterCount}";

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

        await Shell.Current.DisplayAlert("GetHome", $"Contacter {listing.OwnerName} à venir.", "OK");
    }

    [RelayCommand]
    private async Task LoadMoreAsync() =>
        await Shell.Current.DisplayAlert("GetHome", "Chargement par lots (10 suivantes) à venir.", "OK");

    [RelayCommand]
    private async Task GoBackAsync() => await Shell.Current.GoToAsync("..");
}
