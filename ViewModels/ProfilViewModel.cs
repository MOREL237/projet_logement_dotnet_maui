using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetHome.Localization;
using GetHome.Models;
using GetHome.Services;

namespace GetHome.ViewModels;

public partial class ProfilViewModel : ObservableObject
{
    private readonly IDataService _dataService;

    public ProfilViewModel(IDataService dataService)
    {
        _dataService = dataService;
        LocalizationService.Instance.PropertyChanged += OnCultureChanged;
    }

    private void OnCultureChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        // Rafraîchit les textes liés à la langue et les libellés statiques du profil.
        OnPropertyChanged(nameof(LanguageText));
        OnPropertyChanged(nameof(IsFrenchSelected));
        OnPropertyChanged(nameof(IsEnglishSelected));
        OnPropertyChanged(nameof(ModeLabel));
        OnPropertyChanged(nameof(DossierCompletionText));
        OnPropertyChanged(nameof(CacheSizeText));
        OnPropertyChanged(nameof(AppVersionText));
        OnPropertyChanged(nameof(LanguageRowText));
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasProfile))]
    private UserProfile? profile;

    public bool HasProfile => Profile is not null;

    // ---- Mode ----
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsOwnerModeSelected))]
    [NotifyPropertyChangedFor(nameof(IsTenantModeSelected))]
    [NotifyPropertyChangedFor(nameof(ModeLabel))]
    private bool isOwnerMode;

    public bool IsOwnerModeSelected => IsOwnerMode;
    public bool IsTenantModeSelected => !IsOwnerMode;
    public string ModeLabel => IsOwnerMode ? LocalizationService.Instance["Profil_ModeOwner"] : LocalizationService.Instance["Profil_ModeTenant"];

    // ---- Réglages ----
    [ObservableProperty] private bool darkMode;
    [ObservableProperty] private bool lowDataDisplay = true;
    [ObservableProperty] private bool alertsNewHomes = true;
    [ObservableProperty] private bool messageNotifications = true;

    // ---- Textes dérivés ----
    public string DossierCompletionText => $"{LocalizationService.Instance["Profil_DossierCompletion"]}{Profile?.DossierCompletion ?? 0}%";
    public string CacheSizeText => $"{Profile?.CacheSizeMb ?? 0:0.#} Mo stockés sur ce terminal";
    public string LanguageText => LocalizationService.Instance.IsFrench ? "Français (FR)" : "English (EN)";
    public string LanguageRowText => LocalizationService.Instance.IsFrench ? "Langue de l'application" : "App language";
    public bool IsFrenchSelected => LocalizationService.Instance.IsFrench;
    public bool IsEnglishSelected => LocalizationService.Instance.IsEnglish;
    public string AppVersionText => $"GetHome {Profile?.AppVersion ?? "v1.0.0"} · Écoconception numérique certifiée";
    public string CarbonLabel => Profile?.CarbonLabel ?? string.Empty;

    partial void OnProfileChanged(UserProfile? value)
    {
        OnPropertyChanged(nameof(DossierCompletionText));
        OnPropertyChanged(nameof(CacheSizeText));
        OnPropertyChanged(nameof(AppVersionText));
        OnPropertyChanged(nameof(CarbonLabel));
    }

    [RelayCommand]
    private async Task LoadAsync()
    {
        Profile = await _dataService.GetProfileAsync();
        IsOwnerMode = Profile.IsOwnerMode;
    }

    [RelayCommand] private void SelectOwnerMode() => IsOwnerMode = true;
    [RelayCommand] private void SelectTenantMode() => IsOwnerMode = false;

    [RelayCommand]
    private async Task PublishAsync() => await Shell.Current.GoToAsync("publier");

    [RelayCommand]
    private async Task EditInfoAsync() =>
        await Shell.Current.DisplayAlert("Profil", "Modification des informations à venir.", "OK");

    [RelayCommand]
    private async Task ClearCacheAsync() =>
        await Shell.Current.DisplayAlert("Profil", "Cache local purgé (simulation).", "OK");

    [RelayCommand]
    private async Task SecurityAsync() =>
        await Shell.Current.DisplayAlert("Profil", "Sécurité et mot de passe à venir.", "OK");

    [RelayCommand]
    private async Task PrivacyAsync() =>
        await Shell.Current.DisplayAlert("Profil", "Confidentialité & données : zéro tracker tiers · loi camerounaise n° 2010/012 (ANCY).", "OK");

    [RelayCommand]
    private void ToggleLanguage() => LocalizationService.Instance.Toggle();

    [RelayCommand]
    private async Task LogoutAsync()
    {
        var confirmed = await Shell.Current.DisplayAlert("Déconnexion", "Voulez-vous vous déconnecter ?", "Se déconnecter", "Annuler");
        if (confirmed)
            await Shell.Current.GoToAsync("//login");
    }

    [RelayCommand]
    private async Task DeleteAccountAsync()
    {
        var confirmed = await Shell.Current.DisplayAlert("Supprimer le compte",
            "Cette action supprime votre compte et vos données certifiées. Continuer ?", "Supprimer", "Annuler");
        if (confirmed)
            await Shell.Current.DisplayAlert("Profil", "Demande de suppression enregistrée (simulation).", "OK");
    }
}
