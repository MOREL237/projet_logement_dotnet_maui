using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetHome.Models;
using GetHome.Services;

namespace GetHome.ViewModels;

public partial class FavorisViewModel : ObservableObject
{
    private readonly IDataService _dataService;

    public FavorisViewModel(IDataService dataService)
    {
        _dataService = dataService;
    }

    public ObservableCollection<Listing> Favorites { get; } = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsEmpty))]
    private bool isBusy;

    public bool IsEmpty => Favorites.Count == 0;
    public string CountText => $"{Favorites.Count} logement{(Favorites.Count > 1 ? "s" : "")} sauvegardé{(Favorites.Count > 1 ? "s" : "")}";

    [RelayCommand]
    private async Task LoadAsync()
    {
        if (IsBusy)
            return;

        IsBusy = true;
        try
        {
            Favorites.Clear();
            foreach (var listing in await _dataService.GetFavoritesAsync())
                Favorites.Add(listing);

            RefreshSummary();
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void RefreshSummary()
    {
        OnPropertyChanged(nameof(IsEmpty));
        OnPropertyChanged(nameof(CountText));
    }

    [RelayCommand]
    private async Task OpenDetailAsync(Listing? listing)
    {
        if (listing is null)
            return;

        await Shell.Current.GoToAsync($"detail?id={listing.Id}");
    }

    [RelayCommand]
    private async Task RemoveAsync(Listing? listing)
    {
        if (listing is null)
            return;

        Favorites.Remove(listing);
        RefreshSummary();

        await Shell.Current.DisplayAlert("Favoris", $"« {listing.Title} » retiré de vos favoris.", "OK");
    }
}
