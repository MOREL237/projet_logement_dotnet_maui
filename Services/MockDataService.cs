using GetHome.Models;
using Microsoft.Maui.Graphics;

namespace GetHome.Services;

// Données simulées en mémoire, adaptées au marché camerounais (Douala/Yaoundé).
// À remplacer par un vrai service (API) plus tard.
public class MockDataService : IDataService
{
    public string CurrentUserName => "Morel";
    public int ActiveAlerts => 3;

    private static readonly List<Listing> AllListings = new()
    {
        new Listing
        {
            Id = 1,
            Title = "Studio meublé",
            Neighborhood = "Bonapriso",
            Price = 25000,
            PriceUnit = "/ nuit",
            Surface = 28,
            Rooms = "1 pièce",
            Features = new[] { "Meublé design", "28 m²" },
            EcoClass = "B",
            Floor = "3e étage",
            Availability = "Dispo le 1er Mai",
            OwnerName = "Marc D.",
            OwnerInitials = "MD",
            OwnerVerified = true,
            OwnerListingsCount = 4,
            ImageTint = Color.FromArgb("#D8DEE9"),
            AvailabilityDetail = "Disponibilité : 24h/24h",
            Bedrooms = "1 chambre",
            Amenities = new[] { "Wi-Fi", "Climatisation", "Cuisine équipée", "Groupe électrogène" },
            Highlight = "Restaurant-bar-snack",
            Description = "Charmant studio meublé situé à Bonapriso, dans un quartier calme et sécurisé. Entièrement équipé pour un séjour confortable : climatisation, Wi-Fi, cuisine équipée. Proche des commerces, restaurants et axes principaux. Idéal pour les séjours de courte et longue durée."
        },
        new Listing
        {
            Id = 2,
            Title = "Studio",
            Neighborhood = "Logpom",
            Price = 15000,
            PriceUnit = "/ nuit",
            Surface = 36,
            Rooms = "2 pièces",
            Features = new[] { "Cour calme", "Cuisine équipée" },
            EcoClass = "C",
            Floor = "Rez-de-chaussée",
            Availability = "Dispo immédiate",
            AvailableNow = true,
            OwnerName = "Sophie L.",
            OwnerInitials = "SL",
            OwnerVerified = true,
            OwnerListingsCount = 2,
            ImageTint = Color.FromArgb("#E9DECD"),
            AvailabilityDetail = "Disponibilité : immédiate",
            Bedrooms = "1 chambre",
            Amenities = new[] { "Wi-Fi", "Cour calme", "Forage (eau)" },
            Description = "Studio lumineux sur cour calme à Logpom, cuisine équipée, proche des marchés et des transports."
        },
        new Listing
        {
            Id = 3,
            Title = "Charmant 2 pièces meublé — haut standing",
            Neighborhood = "Bonamoussadi",
            Price = 50000,
            PriceUnit = "/ mois",
            Surface = 32,
            Rooms = "2 pièces",
            Features = new[] { "Meublé", "2e étage" },
            EcoClass = "B",
            Floor = "2e étage",
            Availability = "Dispo immédiate",
            AvailableNow = true,
            OwnerName = "Marc D.",
            OwnerInitials = "MD",
            OwnerVerified = true,
            OwnerListingsCount = 4,
            ImageTint = Color.FromArgb("#CFE0E8"),
            AvailabilityDetail = "Disponibilité : immédiate",
            Bedrooms = "1 chambre",
            Amenities = new[] { "Wi-Fi", "Climatisation", "Gardiennage" },
            Description = "Charmant 2 pièces meublé de haut standing à Bonamoussadi, quartier résidentiel calme. Appartement entièrement équipé, proche des commerces et axes principaux."
        },
        new Listing
        {
            Id = 4,
            Title = "Appartement 3 pièces",
            Neighborhood = "Kotto",
            Price = 45000,
            PriceUnit = "/ mois",
            Surface = 41,
            Rooms = "3 pièces",
            Features = new[] { "Cuisine équipée", "Calme sur cour" },
            EcoClass = "C",
            Floor = "1er étage",
            Availability = "Visite vidéo dispo",
            OwnerName = "Pierre M.",
            OwnerInitials = "PM",
            OwnerVerified = true,
            OwnerListingsCount = 1,
            ImageTint = Color.FromArgb("#E4E0D3"),
            AvailabilityDetail = "Disponibilité : sur rendez-vous",
            Bedrooms = "2 chambres",
            Amenities = new[] { "Wi-Fi", "Cuisine équipée", "Calme", "Compteur ENEO prépayé" },
            Description = "Appartement 3 pièces à Kotto, à 30 m de la route. Calme sur cour, cuisine équipée."
        },
        new Listing
        {
            Id = 5,
            Title = "Studio meublé",
            Neighborhood = "Makepé",
            Price = 50000,
            PriceUnit = "/ mois",
            Surface = 32,
            Rooms = "2 pièces",
            Features = new[] { "Meublé", "2e étage" },
            EcoClass = "B",
            Floor = "2e étage",
            Availability = "Dispo immédiate",
            AvailableNow = true,
            OwnerName = "Sophie L.",
            OwnerInitials = "SL",
            OwnerVerified = true,
            OwnerListingsCount = 2,
            ImageTint = Color.FromArgb("#D8DEE9"),
            AvailabilityDetail = "Disponibilité : immédiate",
            Bedrooms = "1 chambre",
            Amenities = new[] { "Wi-Fi", "Meublé", "Groupe électrogène" },
            Description = "Studio meublé à Makepé, à 200 m du goudron. Idéal pour un séjour courte ou longue durée."
        },
        new Listing
        {
            Id = 6,
            Title = "Appartement meublé avec balcon",
            Neighborhood = "Bonamoussadi",
            Price = 120000,
            PriceUnit = "/ mois",
            Surface = 65,
            Rooms = "3 pièces",
            Features = new[] { "Meublé", "Balcon" },
            EcoClass = "A",
            Floor = "4e étage",
            Availability = "Visite confirmée · Sam. 14h",
            OwnerName = "Nadia T.",
            OwnerInitials = "NT",
            OwnerVerified = true,
            OwnerListingsCount = 3,
            ImageTint = Color.FromArgb("#DDD6E8"),
            AvailabilityDetail = "Disponibilité : sur rendez-vous",
            Bedrooms = "2 chambres",
            Amenities = new[] { "Wi-Fi", "Balcon", "Ascenseur" },
            Description = "Appartement meublé haut standing à Bonamoussadi avec balcon, 4e étage avec ascenseur."
        }
    };

    public async Task<IReadOnlyList<SavedSearch>> GetSavedSearchesAsync()
    {
        await Task.Delay(150);

        return new List<SavedSearch>
        {
            new()
            {
                Id = 1,
                Title = "Bonapriso · Studio · max 50 000 FCFA",
                Summary = "Makepé · Studio · balcon",
                NewCount = 12,
                AlertActive = true
            },
            new()
            {
                Id = 2,
                Title = "Makepé · T2 · meublé",
                Summary = "Actualisé il y a 2h · 3 offres",
                NewCount = 3,
                AlertActive = true
            }
        };
    }

    public async Task<IReadOnlyList<Listing>> GetRecentListingsAsync()
    {
        await Task.Delay(200);
        return AllListings.Take(2).ToList();
    }

    public async Task<IReadOnlyList<Listing>> GetListingsAsync()
    {
        await Task.Delay(250);
        return AllListings.ToList();
    }

    public async Task<Listing?> GetListingAsync(int id)
    {
        await Task.Delay(120);
        return AllListings.FirstOrDefault(l => l.Id == id);
    }

    public async Task<IReadOnlyList<Conversation>> GetConversationsAsync()
    {
        await Task.Delay(180);

        return new List<Conversation>
        {
            new()
            {
                Id = 1,
                OwnerName = "Marc D.",
                OwnerRole = "Propriétaire",
                AvatarInitials = "MD",
                TimeLabel = "10:42",
                Preview = "Bonjour Morel, votre dossier complet m'intéresse. Seriez-vous disponible jeudi ?",
                ListingLabel = "2p meublé · 50 000 FCFA",
                StatusLabel = "Visite proposée",
                QuickReply = "Parfait pour jeudi 18h !",
                IsUnread = true,
                IsVisitPlanned = true,
                ImageTint = Color.FromArgb("#CFE0E8")
            },
            new()
            {
                Id = 2,
                OwnerName = "Sophie L.",
                OwnerRole = "Bailleur certifié",
                AvatarInitials = "SL",
                TimeLabel = "Hier",
                Preview = "Dossier bien reçu, je reviens vers vous rapidement.",
                ListingLabel = "Studio Makepé",
                StatusLabel = "Dossier transmis",
                IsUnread = false,
                IsVisitPlanned = false,
                ImageTint = Color.FromArgb("#E9DECD")
            },
            new()
            {
                Id = 3,
                OwnerName = "Pierre M.",
                OwnerRole = "Propriétaire",
                AvatarInitials = "PM",
                TimeLabel = "24 avr.",
                Preview = "La visite est bien confirmée à Kotto.",
                ListingLabel = "Appart · 120 000 FCFA",
                StatusLabel = "Visite confirmée · Sam. 14h",
                IsUnread = false,
                IsVisitPlanned = true,
                ImageTint = Color.FromArgb("#E4E0D3")
            },
            new()
            {
                Id = 4,
                OwnerName = "Nadia T.",
                OwnerRole = "Propriétaire",
                AvatarInitials = "NT",
                TimeLabel = "22 avr.",
                Preview = "Bonjour, le logement est toujours disponible. Souhaitez-vous visiter ?",
                ListingLabel = "3p meublé · 120 000 FCFA",
                StatusLabel = "Nouveau message",
                IsUnread = false,
                IsVisitPlanned = false,
                ImageTint = Color.FromArgb("#DDD6E8")
            }
        };
    }

    public async Task<IReadOnlyList<Listing>> GetFavoritesAsync()
    {
        await Task.Delay(150);
        return AllListings.Where(l => l.Id is 3 or 6).ToList();
    }

    public async Task<UserProfile> GetProfileAsync()
    {
        await Task.Delay(120);

        return new UserProfile
        {
            FullName = "Morel Jose",
            Initials = "TM",
            Email = "moreljose@gmail.com",
            Phone = "+237 650 059 205",
            GetHomeId = "GH-94820",
            IsOwnerMode = true,
            DossierCompletion = 100,
            DossierItems = new[] { "Identité (CNI)", "Revenus", "Caution" },
            CacheSizeMb = 12.4,
            CarbonLabel = "0,12 g CO₂e / session · Serveurs hébergés localement (Cameroun)"
        };
    }
}
