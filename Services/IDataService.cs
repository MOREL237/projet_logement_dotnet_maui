using GetHome.Models;

namespace GetHome.Services;

public interface IDataService
{
    string CurrentUserName { get; }
    int ActiveAlerts { get; }

    Task<IReadOnlyList<SavedSearch>> GetSavedSearchesAsync();
    Task<IReadOnlyList<Listing>> GetRecentListingsAsync();
    Task<IReadOnlyList<Listing>> GetListingsAsync();
    Task<Listing?> GetListingAsync(int id);
    Task<IReadOnlyList<Conversation>> GetConversationsAsync();
    Task<IReadOnlyList<Listing>> GetFavoritesAsync();
    Task<UserProfile> GetProfileAsync();
}
