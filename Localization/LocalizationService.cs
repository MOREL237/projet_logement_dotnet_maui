using System.ComponentModel;
using System.Globalization;

namespace GetHome.Localization;

// Service de localisation partagé. Exposé comme source de binding via l'indexeur.
public sealed class LocalizationService : INotifyPropertyChanged
{
    public static LocalizationService Instance { get; } = new();

    private const string PreferenceKey = "app_culture";
    private string _culture = "fr";

    private LocalizationService()
    {
        try
        {
            var saved = Preferences.Default.Get(PreferenceKey, "fr");
            if (Strings.Supported.Contains(saved))
                _culture = saved;
        }
        catch
        {
            // Preferences indisponible avant l'initialisation MAUI : on reste en FR.
        }

        ApplyCulture();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public string Culture
    {
        get => _culture;
        set
        {
            if (_culture == value || !Strings.Supported.Contains(value))
                return;

            _culture = value;

            try
            {
                Preferences.Default.Set(PreferenceKey, value);
            }
            catch
            {
                // ignoré
            }

            ApplyCulture();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Culture)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Item[]"));
        }
    }

    public bool IsFrench => _culture == "fr";
    public bool IsEnglish => _culture == "en";

    // Indexeur utilisé par TranslateExtension : binding "[Clé]".
    public string this[string key] => Strings.Get(key, _culture);

    public void Toggle()
    {
        Culture = IsFrench ? "en" : "fr";
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Item[]"));
    }

    private void ApplyCulture()
    {
        try
        {
            var culture = new CultureInfo(_culture);
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
        }
        catch
        {
            // ignoré
        }
    }
}
