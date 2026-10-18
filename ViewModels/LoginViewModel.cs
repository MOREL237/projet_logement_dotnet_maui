using System.Text.RegularExpressions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetHome.Localization;

namespace GetHome.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    // ---- Champs ----
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
    private string email = string.Empty;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
    private string password = string.Empty;

    // ---- Erreurs de champs ----
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasEmailError))]
    private string emailError = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasPasswordError))]
    private string passwordError = string.Empty;

    // ---- Erreur générale (bannière) ----
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasError))]
    private string errorMessage = string.Empty;

    // ---- États ----
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
    [NotifyPropertyChangedFor(nameof(LoginButtonText))]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    private bool isBusy;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PasswordToggleIcon))]
    private bool isPasswordHidden = true;

    public bool HasEmailError => !string.IsNullOrEmpty(EmailError);
    public bool HasPasswordError => !string.IsNullOrEmpty(PasswordError);
    public bool IsNotBusy => !IsBusy;
    public bool HasError => !string.IsNullOrEmpty(ErrorMessage);
    public string LoginButtonText => IsBusy ? LocalizationService.Instance["Login_Submitting"] : LocalizationService.Instance["Login_Submit"];
    public string PasswordToggleIcon => IsPasswordHidden ? "eye.svg" : "eye_off.svg";

    // Le bouton est désactivé tant que les champs sont vides ou pendant le chargement
    private bool CanLogin() =>
        !IsBusy && !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password);

    [RelayCommand]
    private void TogglePassword() => IsPasswordHidden = !IsPasswordHidden;

    [RelayCommand(CanExecute = nameof(CanLogin))]
    private async Task LoginAsync()
    {
        ErrorMessage = string.Empty;

        if (!Validate())
            return;

        try
        {
            IsBusy = true;

            // Données fictives : à remplacer plus tard par un vrai service
            await Task.Delay(1200);

            if (Email.Trim().ToLower() == "test@gethome.com" && Password == "123456")
            {
                await Shell.Current.GoToAsync("//main");
            }
            else
            {
                ErrorMessage = LocalizationService.Instance["Login_ErrInvalid"];
            }
        }
        catch (Exception)
        {
            ErrorMessage = LocalizationService.Instance["Login_ErrNetwork"];
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task ForgotPasswordAsync() =>
        await Shell.Current.GoToAsync("//forgot");

    [RelayCommand]
    private async Task GoToRegisterAsync() =>
        await Shell.Current.GoToAsync("//register");

    private bool Validate()
    {
        EmailError = string.Empty;
        PasswordError = string.Empty;

        if (!Regex.IsMatch(Email.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            EmailError = LocalizationService.Instance["Login_ErrEmail"];

        if (Password.Length < 6)
            PasswordError = LocalizationService.Instance["Login_ErrPassword"];

        return !HasEmailError && !HasPasswordError;
    }
}
