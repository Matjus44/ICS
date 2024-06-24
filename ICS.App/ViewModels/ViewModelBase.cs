using CommunityToolkit.Mvvm.ComponentModel;
using ICS.App.Options;
using ICS.App.Services.Interfaces;

namespace ICS.App.ViewModels;

public abstract class ViewModelBase : ObservableRecipient, IViewModel
{
    private bool _isRefreshRequired = true;
    protected readonly IMessengerService MessengerService;
    protected readonly AppOptions Options;

    protected ViewModelBase(IMessengerService messengerService, AppOptions options)
        : base(messengerService.Messenger)
    {
        MessengerService = messengerService;
        Options = options;
        IsActive = true;
    }

    public async Task OnAppearingAsync()
    {
        if (_isRefreshRequired)
        {
            await LoadDataAsync();
            _isRefreshRequired = false;
        }
    }

    protected virtual Task LoadDataAsync()
        => Task.CompletedTask;
    
    /// <summary>
    /// Singleton instance of <see cref="SharedData"/>
    /// Used to share data between view models.
    /// </summary>
    public static SharedData Data => SharedData.Instance;
}
