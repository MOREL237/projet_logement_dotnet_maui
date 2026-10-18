using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetHome.Localization;
using GetHome.Models;

namespace GetHome.ViewModels;

public partial class FiltresViewModel : ObservableObject
{
    public FiltresViewModel()
    {
        RoomOptions = new ObservableCollection<SelectOption>
        {
            new("1"), new("2"), new("3"), new("4"), new("5+")
        };

        foreach (var option in RoomOptions)
            option.PropertyChanged += (_, e) =>
            {
                if (e.PropertyName == nameof(SelectOption.IsSelected))
                    OnPropertyChanged(nameof(RoomSelectionText));
            };

        LocalizationService.Instance.PropertyChanged += (_, _) =>
        {
            OnPropertyChanged(nameof(RoomSelectionText));
            OnPropertyChanged(nameof(MedianRentText));
            OnPropertyChanged(nameof(ApplyButtonText));
        };
    }

    // ---- Localisation ----
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsDoualaSelected))]
    [NotifyPropertyChangedFor(nameof(IsYaoundeSelected))]
    [NotifyPropertyChangedFor(nameof(IsAroundMeSelected))]
    private FilterCity city = FilterCity.Douala;

    public bool IsDoualaSelected => City == FilterCity.Douala;
    public bool IsYaoundeSelected => City == FilterCity.Yaounde;
    public bool IsAroundMeSelected => City == FilterCity.AutourDeMoi;

    [ObservableProperty]
    private string locationQuery = string.Empty;

    // ---- Loyer ----
    [ObservableProperty]
    private double minRent = 15000;

    [ObservableProperty]
    private double maxRent = 75000;

    public string MedianRentText => LocalizationService.Instance["Filtres_MedianRent"];

    // ---- Type d'habitat ----
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsStudioSelected))]
    [NotifyPropertyChangedFor(nameof(IsAppartementSelected))]
    [NotifyPropertyChangedFor(nameof(IsMaisonSelected))]
    [NotifyPropertyChangedFor(nameof(IsColocationSelected))]
    private PropertyKind propertyKind = PropertyKind.Studio;

    public bool IsStudioSelected => PropertyKind == PropertyKind.Studio;
    public bool IsAppartementSelected => PropertyKind == PropertyKind.Appartement;
    public bool IsMaisonSelected => PropertyKind == PropertyKind.Maison;
    public bool IsColocationSelected => PropertyKind == PropertyKind.Colocation;

    // ---- Nombre de pièces ----
    public ObservableCollection<SelectOption> RoomOptions { get; }

    public string RoomSelectionText
    {
        get
        {
            var count = RoomOptions.Count(o => o.IsSelected);
            return count == 0
            ? LocalizationService.Instance["Filtres_NoneSelected"]
            : string.Format(LocalizationService.Instance["Filtres_SelectedCount"], count);
        }
    }

    // ---- Agencement & mobilier ----
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsIndifferentSelected))]
    [NotifyPropertyChangedFor(nameof(IsMeubleSelected))]
    [NotifyPropertyChangedFor(nameof(IsNonMeubleSelected))]
    private FurnishingKind furnishing = FurnishingKind.Indifferent;

    public bool IsIndifferentSelected => Furnishing == FurnishingKind.Indifferent;
    public bool IsMeubleSelected => Furnishing == FurnishingKind.Meuble;
    public bool IsNonMeubleSelected => Furnishing == FurnishingKind.NonMeuble;

    // ---- Éco-performance & confort ----
    [ObservableProperty] private bool ecoSobre;
    [ObservableProperty] private bool lowConsumption;
    [ObservableProperty] private bool noElevator;
    [ObservableProperty] private bool balcony;

    // ---- Résultat ----
    public int ResultCount { get; } = 24;
    public string ApplyButtonText => string.Format(LocalizationService.Instance["Filtres_Apply"], ResultCount);

    // ---- Commandes ----
    [RelayCommand] private void SelectDouala() => City = FilterCity.Douala;
    [RelayCommand] private void SelectYaounde() => City = FilterCity.Yaounde;
    [RelayCommand] private void SelectAroundMe() => City = FilterCity.AutourDeMoi;

    [RelayCommand] private void SelectStudio() => PropertyKind = PropertyKind.Studio;
    [RelayCommand] private void SelectAppartement() => PropertyKind = PropertyKind.Appartement;
    [RelayCommand] private void SelectMaison() => PropertyKind = PropertyKind.Maison;
    [RelayCommand] private void SelectColocation() => PropertyKind = PropertyKind.Colocation;

    [RelayCommand] private void SelectIndifferent() => Furnishing = FurnishingKind.Indifferent;
    [RelayCommand] private void SelectMeuble() => Furnishing = FurnishingKind.Meuble;
    [RelayCommand] private void SelectNonMeuble() => Furnishing = FurnishingKind.NonMeuble;

    [RelayCommand]
    private void ToggleRoom(SelectOption? option)
    {
        if (option is null)
            return;

        option.IsSelected = !option.IsSelected;
        OnPropertyChanged(nameof(RoomSelectionText));
    }

    [RelayCommand]
    private async Task ApplyAsync() =>
        await Shell.Current.GoToAsync("resultats");

    [RelayCommand]
    private void Reset()
    {
        City = FilterCity.Douala;
        PropertyKind = PropertyKind.Studio;
        Furnishing = FurnishingKind.Indifferent;
        LocationQuery = string.Empty;
        MinRent = 15000;
        MaxRent = 75000;
        EcoSobre = LowConsumption = NoElevator = Balcony = false;

        foreach (var option in RoomOptions)
            option.IsSelected = false;

        OnPropertyChanged(nameof(RoomSelectionText));
    }

    [RelayCommand]
    private async Task GoBackAsync() =>
        await Shell.Current.GoToAsync("..");
}
