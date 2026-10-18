namespace GetHome.Localization;

// Table de chaînes FR/EN. FR est la langue de référence (Cameroun francophone).
// Une clé absente dans la culture demandée retombe sur le FR, puis sur la clé brute.
public static class Strings
{
    public static readonly IReadOnlyList<string> Supported = new[] { "fr", "en" };

    private static readonly Dictionary<string, Dictionary<string, string>> Map = new()
    {
        ["fr"] = new()
        {
            // ---- Commun ----
            ["Common_Email"] = "Adresse e-mail",
            ["Common_EmailPlaceholder"] = "example@gmail.com",
            ["Common_Password"] = "Mot de passe",
            ["Common_PasswordPlaceholder"] = "mot de passe",
            ["Common_ShowPassword"] = "Afficher ou masquer le mot de passe",

            // ---- Barre d'onglets ----
            ["Shell_Recherche"] = "Recherche",
            ["Shell_Favoris"] = "Favoris",
            ["Shell_Messages"] = "Messages",
            ["Shell_Profil"] = "Profil",

            // ---- Connexion ----
            ["Login_Title"] = "Connexion",
            ["Login_Forgot"] = "Mot de passe oublié ?",
            ["Login_Submit"] = "Se connecter",
            ["Login_Submitting"] = "Connexion…",
            ["Login_NoAccount"] = "Pas encore de compte ?",
            ["Login_CreateAccount"] = "Créer un compte",
            ["Login_ErrInvalid"] = "E-mail ou mot de passe incorrect.",
            ["Login_ErrEmail"] = "Saisissez une adresse e-mail valide.",
            ["Login_ErrPassword"] = "Le mot de passe doit contenir au moins 6 caractères.",
            ["Login_ErrNetwork"] = "Connexion impossible. Vérifiez votre réseau et réessayez.",

            // ---- Mot de passe oublié ----
            ["Forgot_Security"] = "SÉCURITÉ",
            ["Forgot_AccessRecovery"] = "Récupération d'accès",
            ["Forgot_Title"] = "Mot de passe oublié",
            ["Forgot_Intro"] = "Indiquez l'adresse e-mail associée à votre compte GetHome. Vous recevrez un lien de réinitialisation sécurisé instantané.",
            ["Forgot_Send"] = "Envoyer le lien de réinitialisation",
            ["Forgot_Sending"] = "Envoi…",
            ["Forgot_Sent"] = "Lien envoyé !",
            ["Forgot_SentDetail"] = "Vérifiez votre boîte de réception. Expire dans 15 minutes.",
            ["Forgot_EcoTitle"] = "Écoconception sobre",
            ["Forgot_EcoDetail"] = "Aucune donnée superflue conservée.",
            ["Forgot_BackToLogin"] = "Retour à la connexion",
            ["Forgot_ErrNetwork"] = "Envoi impossible. Vérifiez votre réseau et réessayez.",
            ["Forgot_ErrEmail"] = "Format d'adresse e-mail invalide",

            // ---- Profil (langue) ----
            ["Profil_Language"] = "Langue de l'application",
            ["Profil_LanguageFR"] = "Français (FR)",
            ["Profil_LanguageEN"] = "English (EN)",

            // ---- Inscription ----
            ["Register_Title"] = "Inscription",
            ["Register_Tagline"] = "Rejoignez le réseau direct locataires & bailleurs.",
            ["Register_Seeder"] = "Chercheur",
            ["Register_SeederSub"] = "Locataire",
            ["Register_Owner"] = "Propriétaire",
            ["Register_OwnerSub"] = "Bailleur",
            ["Register_FullName"] = "Nom complet",
            ["Register_FullNamePlaceholder"] = "Votre nom et prénom",
            ["Register_Email"] = "Adresse e-mail (requis)",
            ["Register_Phone"] = "Numéro de téléphone",
            ["Register_Confirm"] = "Confirmation du mot de passe",
            ["Register_ConfirmPlaceholder"] = "Ressaisissez votre mot de passe",
            ["Register_PasswordRules"] = "8 car. · 1 chiffre · 1 majuscule",
            ["Register_Terms"] = "J'accepte les",
            ["Register_TermsLink"] = "conditions générales d'utilisation",
            ["Register_Terms2"] = " et la ",
            ["Register_Charter"] = "charte de sobriété numérique",
            ["Register_Terms3"] = ".",
            ["Register_Already"] = "Déjà inscrit ?",
            ["Register_Login"] = "Se connecter",
            ["Register_ErrEmail"] = "Format d'adresse e-mail invalide",
            ["Register_ErrName"] = "Indiquez votre nom complet.",
            ["Register_ErrPhone"] = "Indiquez un numéro de téléphone valide.",
            ["Register_ErrPassword"] = "Le mot de passe doit contenir au moins 8 caractères, 1 chiffre et 1 majuscule.",
            ["Register_ErrConfirm"] = "Les mots de passe ne correspondent pas.",
            ["Register_ErrNetwork"] = "Inscription impossible. Vérifiez votre réseau et réessayez.",
            ["Register_Submit"] = "Finaliser l'inscription",
            ["Register_Submitting"] = "Inscription…",

            // ---- Profil ----
            ["Profil_Title"] = "Profil",
            ["Profil_Certified"] = "Compte certifié GetHome ID · ",
            ["Profil_Owner"] = "Propriétaire",
            ["Profil_Tenant"] = "Locataire",
            ["Profil_ModeOwner"] = "Mode Propriétaire",
            ["Profil_ModeTenant"] = "Mode Locataire actif",
            ["Profil_EditInfo"] = "Modifier mes informations",
            ["Profil_Dossier"] = "Dossier de candidature",
            ["Profil_DossierCompletion"] = "Complet · ",
            ["Profil_EnergySection"] = "ÉCONOMIE D'ÉNERGIE & AFFICHAGE",
            ["Profil_Sobriety"] = "Sobriété A+",
            ["Profil_DarkMode"] = "Mode sombre (Économie OLED)",
            ["Profil_DarkModeDesc"] = "Réduit la consommation de batterie jusqu'à 60% sur écran OLED.",
            ["Profil_LowData"] = "Affichage basse consommation",
            ["Profil_LowDataDesc"] = "Limite le chargement automatique des images HD en réseau mobile pour limiter l'empreinte data.",
            ["Profil_PurgeCache"] = "Purge du cache local",
            ["Profil_Clear"] = "Vider",
            ["Profil_NotifSection"] = "NOTIFICATIONS & ALERTES",
            ["Profil_NotifAlerts"] = "Alertes nouveaux logements",
            ["Profil_NotifAlertsDesc"] = "Fréquence sobre : 1 notification groupée/jour maximum à 18h.",
            ["Profil_NotifMessages"] = "Notifications de messagerie",
            ["Profil_NotifMessagesDesc"] = "En direct exclusivement lors d'une invitation à une visite.",
            ["Profil_PrefsSection"] = "PRÉFÉRENCES GÉNÉRALES & SÉCURITÉ",
            ["Profil_Security"] = "Sécurité et mot de passe",
            ["Profil_Privacy"] = "Confidentialité & Données",
            ["Profil_PrivacyDesc"] = "Politique stricte : Zéro Tracker tiers · Loi n° 2010/012 (ANCY)",
            ["Profil_Publish"] = "Publier une annonce",
            ["Profil_Logout"] = "Se déconnecter",
            ["Profil_Delete"] = "Supprimer mon compte et mes données certifiées",
            ["Profil_LanguageHint"] = "Cliquez pour basculer la langue",

            // ---- Accueil ----
            ["Accueil_Title"] = "Recherche",
            ["Accueil_Tagline"] = "Mise à jour directe · Propriétaires vérifiés",
            ["Accueil_SavedSearches"] = "Recherches enregistrées",
            ["Accueil_EcoAlertActive"] = "Éco-alerte active",
            ["Accueil_Refresh"] = "Consulter",
            ["Accueil_RecentListings"] = "Annonces récentes",
            ["Accueil_VerifiedNoMiddle"] = "Propriétaires vérifiés sans intermédiaires",
            ["Accueil_AvailableNow"] = "Dispo immédiate",
            ["Accueil_FilterByDate"] = "Filtrer par date",
            ["Accueil_ViewListing"] = "Voir l'annonce",
            ["Accueil_Contact"] = "Contacter",
            ["Accueil_EcoNoteTitle"] = "Objectif < 3 clics pour contacter",
            ["Accueil_EcoNoteDetail"] = "Dossier vérifié automatiquement transmis aux propriétaires sans spam.",
            ["Accueil_RefreshFailed"] = "Impossible de rafraîchir les annonces. Réessayez.",
            ["Accueil_OpenListingFailed"] = "Impossible d'ouvrir le détail du logement.",
            ["Accueil_OpenSearchFailed"] = "Impossible d'ouvrir cette recherche enregistrée.",
            ["Accueil_NoSaved"] = "Aucune recherche enregistrée pour le moment."
        },

        ["en"] = new()
        {
            // ---- Common ----
            ["Common_Email"] = "Email address",
            ["Common_EmailPlaceholder"] = "example@gmail.com",
            ["Common_Password"] = "Password",
            ["Common_PasswordPlaceholder"] = "password",
            ["Common_ShowPassword"] = "Show or hide password",

            // ---- Tab bar ----
            ["Shell_Recherche"] = "Search",
            ["Shell_Favoris"] = "Favorites",
            ["Shell_Messages"] = "Messages",
            ["Shell_Profil"] = "Profile",

            // ---- Sign in ----
            ["Login_Title"] = "Sign in",
            ["Login_Forgot"] = "Forgot password?",
            ["Login_Submit"] = "Sign in",
            ["Login_Submitting"] = "Signing in…",
            ["Login_NoAccount"] = "No account yet?",
            ["Login_CreateAccount"] = "Create an account",
            ["Login_ErrInvalid"] = "Incorrect email or password.",
            ["Login_ErrEmail"] = "Enter a valid email address.",
            ["Login_ErrPassword"] = "Password must be at least 6 characters.",
            ["Login_ErrNetwork"] = "Unable to sign in. Check your connection and try again.",

            // ---- Forgot password ----
            ["Forgot_Security"] = "SECURITY",
            ["Forgot_AccessRecovery"] = "Account recovery",
            ["Forgot_Title"] = "Forgot password",
            ["Forgot_Intro"] = "Enter the email address linked to your GetHome account. You will receive an instant secure reset link.",
            ["Forgot_Send"] = "Send reset link",
            ["Forgot_Sending"] = "Sending…",
            ["Forgot_Sent"] = "Link sent!",
            ["Forgot_SentDetail"] = "Check your inbox. Expires in 15 minutes.",
            ["Forgot_EcoTitle"] = "Sober eco-design",
            ["Forgot_EcoDetail"] = "No unnecessary data is kept.",
            ["Forgot_BackToLogin"] = "Back to sign in",
            ["Forgot_ErrNetwork"] = "Sending failed. Check your connection and try again.",

            // ---- Profile (language) ----
            ["Profil_Language"] = "App language",
            ["Profil_LanguageFR"] = "Français (FR)",
            ["Profil_LanguageEN"] = "English (EN)",

            // ---- Sign up ----
            ["Register_Title"] = "Sign up",
            ["Register_Tagline"] = "Join the direct tenants & landlords network.",
            ["Register_Seeder"] = "Seeker",
            ["Register_SeederSub"] = "Tenant",
            ["Register_Owner"] = "Owner",
            ["Register_OwnerSub"] = "Landlord",
            ["Register_FullName"] = "Full name",
            ["Register_FullNamePlaceholder"] = "Your first and last name",
            ["Register_Email"] = "Email address (required)",
            ["Register_Phone"] = "Phone number",
            ["Register_Confirm"] = "Confirm password",
            ["Register_ConfirmPlaceholder"] = "Re-enter your password",
            ["Register_PasswordRules"] = "8 chars · 1 digit · 1 uppercase",
            ["Register_Terms"] = "I accept the ",
            ["Register_TermsLink"] = "terms of use",
            ["Register_Terms2"] = " and the ",
            ["Register_Charter"] = "digital sobriety charter",
            ["Register_Terms3"] = ".",
            ["Register_Already"] = "Already registered?",
            ["Register_Login"] = "Sign in",
            ["Register_ErrEmail"] = "Invalid email format",
            ["Register_ErrName"] = "Enter your full name.",
            ["Register_ErrPhone"] = "Enter a valid phone number.",
            ["Register_ErrPassword"] = "Password must be at least 8 characters, with 1 digit and 1 uppercase letter.",
            ["Register_ErrConfirm"] = "Passwords do not match.",
            ["Register_ErrNetwork"] = "Sign-up failed. Check your connection and try again.",
            ["Register_Submit"] = "Complete sign-up",
            ["Register_Submitting"] = "Signing up…",

            // ---- Profile ----
            ["Profil_Title"] = "Profile",
            ["Profil_Certified"] = "Verified GetHome ID account · ",
            ["Profil_Owner"] = "Owner",
            ["Profil_Tenant"] = "Tenant",
            ["Profil_ModeOwner"] = "Owner mode",
            ["Profil_ModeTenant"] = "Active tenant mode",
            ["Profil_EditInfo"] = "Edit my details",
            ["Profil_Dossier"] = "Candidacy file",
            ["Profil_DossierCompletion"] = "Complete · ",
            ["Profil_EnergySection"] = "ENERGY SAVING & RATING",
            ["Profil_Sobriety"] = "Sobriety A+",
            ["Profil_DarkMode"] = "Dark mode (OLED saving)",
            ["Profil_DarkModeDesc"] = "Reduces battery consumption by up to 60% on OLED screens.",
            ["Profil_LowData"] = "Low-consumption display",
            ["Profil_LowDataDesc"] = "Limits automatic HD image loading on mobile networks to reduce data footprint.",
            ["Profil_PurgeCache"] = "Clear local cache",
            ["Profil_Clear"] = "Clear",
            ["Profil_NotifSection"] = "NOTIFICATIONS & ALERTS",
            ["Profil_NotifAlerts"] = "New housing alerts",
            ["Profil_NotifAlertsDesc"] = "Sober frequency: max 1 grouped notification/day at 6pm.",
            ["Profil_NotifMessages"] = "Messaging notifications",
            ["Profil_NotifMessagesDesc"] = "Live only when a visit is arranged.",
            ["Profil_PrefsSection"] = "GENERAL PREFERENCES & SECURITY",
            ["Profil_Security"] = "Security and password",
            ["Profil_Privacy"] = "Privacy & Data",
            ["Profil_PrivacyDesc"] = "Strict policy: No third-party trackers · Law No. 2010/012 (ANCY)",
            ["Profil_Publish"] = "Publish a listing",
            ["Profil_Logout"] = "Sign out",
            ["Profil_Delete"] = "Delete my account and certified data",
            ["Profil_LanguageHint"] = "Tap to switch language",

            // ---- Home ----
            ["Accueil_Title"] = "Search",
            ["Accueil_Tagline"] = "Direct updates · Verified owners",
            ["Accueil_SavedSearches"] = "Saved searches",
            ["Accueil_EcoAlertActive"] = "Eco-alert active",
            ["Accueil_Refresh"] = "Refresh",
            ["Accueil_RecentListings"] = "Recent listings",
            ["Accueil_VerifiedNoMiddle"] = "Verified owners, no middlemen",
            ["Accueil_AvailableNow"] = "Available now",
            ["Accueil_FilterByDate"] = "Filter by date",
            ["Accueil_ViewListing"] = "View listing",
            ["Accueil_Contact"] = "Contact",
            ["Accueil_EcoNoteTitle"] = "Goal: under 3 taps to contact",
            ["Accueil_EcoNoteDetail"] = "Verified file sent automatically to owners, no spam.",
            ["Accueil_RefreshFailed"] = "Could not refresh listings. Try again.",
            ["Accueil_OpenListingFailed"] = "Could not open the listing details.",
            ["Accueil_OpenSearchFailed"] = "Could not open this saved search.",
            ["Accueil_NoSaved"] = "No saved searches yet."
        }
    };

    public static string Get(string key, string culture)
    {
        if (Map.TryGetValue(culture, out var table) && table.TryGetValue(key, out var value))
            return value;

        if (Map["fr"].TryGetValue(key, out var fallback))
            return fallback;

        return key;
    }
}
