using Microsoft.Maui.Graphics;

namespace GetHome.Models;

public class UserProfile
{
    public string FullName { get; set; } = string.Empty;
    public string Initials { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string GetHomeId { get; set; } = string.Empty;
    public bool IsOwnerMode { get; set; }
    public int DossierCompletion { get; set; } = 100;
    public string[] DossierItems { get; set; } = Array.Empty<string>();
    public double CacheSizeMb { get; set; } = 12.4;
    public string AppVersion { get; set; } = "v1.0.4";
    public string CarbonLabel { get; set; } = "0,12 g CO₂e / session · Serveurs hébergés localement (Cameroun)";
    public Color AvatarTint { get; set; } = Color.FromArgb("#CFE0E8");
}
