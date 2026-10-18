namespace GetHome.Models;

public class SavedSearch
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public int NewCount { get; set; }
    public bool AlertActive { get; set; }

    public string NewCountText => $"{NewCount} nouvelles annonces";
}
