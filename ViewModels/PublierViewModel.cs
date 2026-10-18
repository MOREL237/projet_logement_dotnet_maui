using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetHome.Localization;
using GetHome.Models;

namespace GetHome.ViewModels;

public partial class PublierViewModel : ObservableObject
{
    public PublierViewModel()
    {
        EcoOptions = new ObservableCollection<SelectOption>
        {
            new("A"), new("B"), new("C"), new("D"), new("E"), new("F"), new("G")
        };
        EcoOptions[1].IsSelected = true;

        LocalizationService.Instance.PropertyChanged += (_, _) => OnPropertyChanged(nameof(EcoSummaryText));
    }

    // ---- Stepper ----
    public int CurrentStep { get; } = 1;

    // ---- Type de bien ----
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsAppartementSelected))]
    [NotifyPropertyChangedFor(nameof(IsStudioSelected))]
    [NotifyPropertyChangedFor(nameof(IsMaisonSelected))]
    private PropertyKind propertyKind = PropertyKind.Appartement;

    public bool IsAppartementSelected => PropertyKind == PropertyKind.Appartement;
    public bool IsStudioSelected => PropertyKind == PropertyKind.Studio;
    public bool IsMaisonSelected => PropertyKind == PropertyKind.Maison;

    // ---- Détails ----
    [ObservableProperty] private string address = string.Empty;
    [ObservableProperty] private string surface = "32";
    [ObservableProperty] private string rooms = "2";
    [ObservableProperty] private string floor = "2";
    [ObservableProperty] private bool hasElevator;

    // ---- Régime de location ----
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsMeubleSelected))]
    [NotifyPropertyChangedFor(nameof(IsNonMeubleSelected))]
    private FurnishingKind furnishing = FurnishingKind.Meuble;

    public bool IsMeubleSelected => Furnishing == FurnishingKind.Meuble;
    public bool IsNonMeubleSelected => Furnishing == FurnishingKind.NonMeuble;

    // ---- Éco-classe ----
    public ObservableCollection<SelectOption> EcoOptions { get; }

    public string EcoSummaryText
    {
        get
        {
            var selected = EcoOptions.FirstOrDefault(o => o.IsSelected);
            return selected is null
                ? LocalizationService.Instance["Publier_EcoNone"]
                : string.Format(LocalizationService.Instance["Publier_EcoValue"], selected.Label);
        }
    }

    // ---- Commandes ----
    [RelayCommand] private void SelectAppartement() => PropertyKind = PropertyKind.Appartement;
    [RelayCommand] private void SelectStudio() => PropertyKind = PropertyKind.Studio;
    [RelayCommand] private void SelectMaison() => PropertyKind = PropertyKind.Maison;

    [RelayCommand] private void SelectMeuble() => Furnishing = FurnishingKind.Meuble;
    [RelayCommand] private void SelectNonMeuble() => Furnishing = FurnishingKind.NonMeuble;

    [RelayCommand]
    private void SelectEco(SelectOption? option)
    {
        if (option is null)
            return;

        foreach (var eco in EcoOptions)
            eco.IsSelected = ReferenceEquals(eco, option);

        OnPropertyChanged(nameof(EcoSummaryText));
    }

    [RelayCommand]
    private async Task ContinueAsync() =>
        await Shell.Current.DisplayAlert("GetHome",
            LocalizationService.Instance["Publier_ContinueSoon"], "OK");

    [RelayCommand]
    private async Task SaveDraftAsync() =>
        await Shell.Current.DisplayAlert("GetHome", LocalizationService.Instance["Publier_DraftSaved"], "OK");

    [RelayCommand]
    private async Task GoBackAsync() => await Shell.Current.GoToAsync("..");
}
