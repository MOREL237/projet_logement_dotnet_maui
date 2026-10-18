using Microsoft.Maui.Graphics;

namespace GetHome.Models;

public class Conversation
{
    public int Id { get; set; }
    public string OwnerName { get; set; } = string.Empty;
    public string OwnerRole { get; set; } = string.Empty;
    public string AvatarInitials { get; set; } = string.Empty;
    public string TimeLabel { get; set; } = string.Empty;
    public string Preview { get; set; } = string.Empty;
    public string ListingLabel { get; set; } = string.Empty;
    public string StatusLabel { get; set; } = string.Empty;
    public string? QuickReply { get; set; }
    public bool IsUnread { get; set; }
    public bool IsVisitPlanned { get; set; }
    public Color ImageTint { get; set; } = Color.FromArgb("#E5E5E5");
}
