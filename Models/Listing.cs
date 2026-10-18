using Microsoft.Maui.Graphics;

namespace GetHome.Models;

public class Listing
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Neighborhood { get; set; } = string.Empty;
    public int Price { get; set; }
    public string PriceUnit { get; set; } = "/ mois";
    public int Surface { get; set; }
    public string Rooms { get; set; } = string.Empty;
    public string[] Features { get; set; } = Array.Empty<string>();
    public string EcoClass { get; set; } = string.Empty;
    public string Floor { get; set; } = string.Empty;
    public string Availability { get; set; } = string.Empty;
    public bool AvailableNow { get; set; }
    public string OwnerName { get; set; } = string.Empty;
    public bool OwnerVerified { get; set; }
    public Color ImageTint { get; set; } = Color.FromArgb("#E5E5E5");

    // Détail d'une annonce
    public int PhotoCount { get; set; } = 4;
    public string AvailabilityDetail { get; set; } = string.Empty;
    public string Bedrooms { get; set; } = string.Empty;
    public string[] Amenities { get; set; } = Array.Empty<string>();
    public string Description { get; set; } = string.Empty;
    public string Highlight { get; set; } = string.Empty;
    public string OwnerInitials { get; set; } = string.Empty;
    public string OwnerResponseTime { get; set; } = "< 2 heures";
    public string OwnerResponseRate { get; set; } = "98 %";
    public int OwnerListingsCount { get; set; } = 4;

    public string PriceText => $"{Price:0} FCFA";
    public string PricePerUnit => $"{Price:0}{PriceUnit}";
    public string FeaturesText => string.Join(" · ", Features);
    public string SurfaceText => $"{Surface} m²";
    public string PhotoCounterText => $"1/{PhotoCount} · flux optimisé";
}
