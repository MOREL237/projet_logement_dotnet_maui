using System.Text.RegularExpressions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetHome.Localization;
using GetHome.Models;

namespace GetHome.ViewModels;

public partial class RegisterViewModel : ObservableObject
{
    // ---- Choix du rôle ----
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsLocataireSelected))]
    [NotifyPropertyChangedFor(nameof(IsProprietaireSelected))]
    private UserRole role = UserRole.Locataire;

    public bool IsLocataireSelected => Role == UserRole.Locataire;
    public bool IsProprietaireSelected => Role == UserRole.Proprietaire;

    // ---- Champs ----
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasNameError))]
    [NotifyPropertyChangedFor(nameof(IsFormValid))]
    [NotifyPropertyChangedFor(nameof(RegisterButtonText))]
    [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
    private string fullName = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasEmailError))]
    [NotifyPropertyChangedFor(nameof(IsFormValid))]
    [NotifyPropertyChangedFor(nameof(RegisterButtonText))]
    [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
    private string email = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasPhoneError))]
    [NotifyPropertyChangedFor(nameof(IsFormValid))]
    [NotifyPropertyChangedFor(nameof(RegisterButtonText))]
    [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
    private string phone = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasPasswordError))]
    [NotifyPropertyChangedFor(nameof(IsFormValid))]
    [NotifyPropertyChangedFor(nameof(RegisterButtonText))]
    [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
    private string password = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasConfirmError))]
    [NotifyPropertyChangedFor(nameof(IsFormValid))]
    [NotifyPropertyChangedFor(nameof(RegisterButtonText))]
    [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
    private string confirmPassword = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsFormValid))]
    [NotifyPropertyChangedFor(nameof(RegisterButtonText))]
    [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
    private bool acceptTerms;

    // ---- Erreurs de champs ----
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasNameError))]
    private string nameError = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasEmailError))]
    private string emailError = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasPhoneError))]
    private string phoneError = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasPasswordError))]
    private string passwordError = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasConfirmError))]
    private string confirmError = string.Empty;

    // ---- Erreur générale (bannière) ----
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasError))]
    private string errorMessage = string.Empty;

    // ---- États ----
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
    [NotifyPropertyChangedFor(nameof(RegisterButtonText))]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    private bool isBusy;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PasswordToggleIcon))]
    private bool isPasswordHidden = true;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ConfirmToggleIcon))]
    private bool isConfirmHidden = true;

    public bool HasNameError => !string.IsNullOrEmpty(NameError);
    public bool HasEmailError => !string.IsNullOrEmpty(EmailError);
    public bool HasPhoneError => !string.IsNullOrEmpty(PhoneError);
    public bool HasPasswordError => !string.IsNullOrEmpty(PasswordError);
    public bool HasConfirmError => !string.IsNullOrEmpty(ConfirmError);
    public bool HasError => !string.IsNullOrEmpty(ErrorMessage);
    public bool IsNotBusy => !IsBusy;
    public string PasswordToggleIcon => IsPasswordHidden ? "eye.svg" : "eye_off.svg";
    public string ConfirmToggleIcon => IsConfirmHidden ? "eye.svg" : "eye_off.svg";

    // Formulaire complet et valide (hors affichage des messages d'erreur)
    public bool IsFormValid =>
        !string.IsNullOrWhiteSpace(FullName) &&
        IsEmailValid(Email) &&
        IsPhoneValid(Phone) &&
        IsPasswordValid(Password) &&
        Password == ConfirmPassword &&
        AcceptTerms;

    public string RegisterButtonText =>
        IsBusy ? LocalizationService.Instance["Register_Submitting"] :
        IsFormValid ? LocalizationService.Instance["Register_Submit"] : LocalizationService.Instance["Register_Submit"] + " (invalide)";

    private bool CanRegister() => !IsBusy && IsFormValid;

    [RelayCommand]
    private void SelectLocataire() => Role = UserRole.Locataire;

    [RelayCommand]
    private void SelectProprietaire() => Role = UserRole.Proprietaire;

    [RelayCommand]
    private void TogglePassword() => IsPasswordHidden = !IsPasswordHidden;

    [RelayCommand]
    private void ToggleConfirm() => IsConfirmHidden = !IsConfirmHidden;

    [RelayCommand(CanExecute = nameof(CanRegister))]
    private async Task RegisterAsync()
    {
        ErrorMessage = string.Empty;

        if (!Validate())
            return;

        try
        {
            IsBusy = true;

            // Données fictives : à remplacer plus tard par un vrai service
            await Task.Delay(1200);

            // À l'inscription, on rejoint directement l'application principale.
            await Shell.Current.GoToAsync("//main");
        }
        catch (Exception)
        {
            ErrorMessage = LocalizationService.Instance["Register_ErrNetwork"];
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task GoToLoginAsync() =>
        await Shell.Current.GoToAsync("//login");

    // Valide l'ensemble des champs et renseigne les messages d'erreur.
    private bool Validate()
    {
        NameError = string.Empty;
        EmailError = string.Empty;
        PhoneError = string.Empty;
        PasswordError = string.Empty;
        ConfirmError = string.Empty;

        if (string.IsNullOrWhiteSpace(FullName))
            NameError = LocalizationService.Instance["Register_ErrName"];

        if (!IsEmailValid(Email))
            EmailError = LocalizationService.Instance["Register_ErrEmail"];

        if (!IsPhoneValid(Phone))
            PhoneError = LocalizationService.Instance["Register_ErrPhone"];

        if (!IsPasswordValid(Password))
            PasswordError = LocalizationService.Instance["Register_ErrPassword"];
        else if (Password != ConfirmPassword)
            ConfirmError = LocalizationService.Instance["Register_ErrConfirm"];

        return !HasNameError && !HasEmailError && !HasPhoneError && !HasPasswordError && !HasConfirmError;
    }

    private static bool IsEmailValid(string value) =>
        Regex.IsMatch(value.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

    private static bool IsPhoneValid(string value) =>
        Regex.IsMatch(Regex.Replace(value, @"[^\d]", ""), @"^\d{8,15}$");

    private static bool IsPasswordValid(string value) =>
        value.Length >= 8 &&
        value.Any(char.IsDigit) &&
        value.Any(char.IsUpper);
}
