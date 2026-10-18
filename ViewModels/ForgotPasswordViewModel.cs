using System.Text.RegularExpressions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetHome.Localization;

namespace GetHome.ViewModels;

public partial class ForgotPasswordViewModel : ObservableObject
{
    // ---- Champs ----
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SendCommand))]
    private string email = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasEmailError))]
    private string emailError = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasError))]
    private string errorMessage = string.Empty;

    // ---- États ----
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SendCommand))]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    [NotifyPropertyChangedFor(nameof(SendButtonText))]
    private bool isBusy;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotSent))]
    private bool isSent;

    public bool HasEmailError => !string.IsNullOrEmpty(EmailError);
    public bool HasError => !string.IsNullOrEmpty(ErrorMessage);
    public bool IsNotBusy => !IsBusy;
    public bool IsNotSent => !IsSent;
    public string SendButtonText => IsBusy ? LocalizationService.Instance["Forgot_Sending"] : LocalizationService.Instance["Forgot_Send"];

    private bool CanSend() => !IsBusy && !string.IsNullOrWhiteSpace(Email);

    [RelayCommand(CanExecute = nameof(CanSend))]
    private async Task SendAsync()
    {
        EmailError = string.Empty;
        ErrorMessage = string.Empty;

        if (!Regex.IsMatch(Email.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            EmailError = LocalizationService.Instance["Forgot_ErrEmail"];
            return;
        }

        try
        {
            IsBusy = true;

            // Données fictives : à remplacer plus tard par un vrai service
            await Task.Delay(1000);

            IsSent = true;
        }
        catch (Exception)
        {
            ErrorMessage = LocalizationService.Instance["Forgot_ErrNetwork"];
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task GoToLoginAsync() =>
        await Shell.Current.GoToAsync("//login");
}
