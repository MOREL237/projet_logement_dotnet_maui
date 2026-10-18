using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetHome.Localization;
using GetHome.Models;
using GetHome.Services;

namespace GetHome.ViewModels;

public partial class MessagesViewModel : ObservableObject
{
    private readonly IDataService _dataService;
    private readonly List<Conversation> _all = new();

    public MessagesViewModel(IDataService dataService)
    {
        _dataService = dataService;
        LocalizationService.Instance.PropertyChanged += (_, _) =>
        {
            OnPropertyChanged(nameof(AllTabText));
            OnPropertyChanged(nameof(UnreadTabText));
            OnPropertyChanged(nameof(VisitsTabText));
        };
    }

    public ObservableCollection<Conversation> Conversations { get; } = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsAllSelected))]
    [NotifyPropertyChangedFor(nameof(IsUnreadSelected))]
    [NotifyPropertyChangedFor(nameof(IsVisitsSelected))]
    private MessageFilter filter = MessageFilter.All;

    [ObservableProperty]
    private string composerText = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AllTabText))]
    private int totalCount;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(UnreadTabText))]
    private int unreadCount;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(VisitsTabText))]
    private int visitsCount;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasConversations))]
    private bool isBusy;

    public bool HasConversations => Conversations.Count > 0;
    public bool IsAllSelected => Filter == MessageFilter.All;
    public bool IsUnreadSelected => Filter == MessageFilter.Unread;
    public bool IsVisitsSelected => Filter == MessageFilter.Visits;

    public string AllTabText => string.Format(LocalizationService.Instance["Messages_TabAll"], TotalCount);
    public string UnreadTabText => string.Format(LocalizationService.Instance["Messages_TabUnread"], UnreadCount);
    public string VisitsTabText => string.Format(LocalizationService.Instance["Messages_TabVisits"], VisitsCount);

    partial void OnFilterChanged(MessageFilter value) => Apply();

    [RelayCommand]
    private async Task LoadAsync()
    {
        if (IsBusy)
            return;

        IsBusy = true;
        try
        {
            _all.Clear();
            _all.AddRange(await _dataService.GetConversationsAsync());

            TotalCount = _all.Count;
            UnreadCount = _all.Count(c => c.IsUnread);
            VisitsCount = _all.Count(c => c.IsVisitPlanned);

            Apply();
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void Apply()
    {
        Conversations.Clear();

        var filtered = Filter switch
        {
            MessageFilter.Unread => _all.Where(c => c.IsUnread),
            MessageFilter.Visits => _all.Where(c => c.IsVisitPlanned),
            _ => _all
        };

        foreach (var conversation in filtered)
            Conversations.Add(conversation);

        OnPropertyChanged(nameof(HasConversations));
    }

    [RelayCommand] private void ShowAll() => Filter = MessageFilter.All;
    [RelayCommand] private void ShowUnread() => Filter = MessageFilter.Unread;
    [RelayCommand] private void ShowVisits() => Filter = MessageFilter.Visits;

    [RelayCommand]
    private async Task OpenAsync(Conversation? conversation)
    {
        if (conversation is null)
            return;

        await Shell.Current.DisplayAlert("GetHome",
            string.Format(LocalizationService.Instance["Messages_OpenSoon"], conversation.OwnerName), "OK");
    }

    [RelayCommand]
    private async Task SendAsync()
    {
        if (string.IsNullOrWhiteSpace(ComposerText))
            return;

        var message = ComposerText;
        ComposerText = string.Empty;

        await Shell.Current.DisplayAlert("GetHome",
            string.Format(LocalizationService.Instance["Messages_Sent"], message), "OK");
    }

    [RelayCommand]
    private async Task ArchiveAllAsync()
    {
        var confirmed = await Shell.Current.DisplayAlert(LocalizationService.Instance["Messages_ArchiveTitle"],
            LocalizationService.Instance["Messages_ArchivePrompt"],
            LocalizationService.Instance["Messages_Archive"],
            LocalizationService.Instance["Messages_Cancel"]);
        if (!confirmed)
            return;

        _all.Clear();
        TotalCount = UnreadCount = VisitsCount = 0;
        Apply();
    }
}
