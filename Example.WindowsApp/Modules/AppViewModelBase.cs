namespace Example.WindowsApp.Modules;

using Smart.Navigation;
using Smart.Windows.ViewModels;

public class AppViewModelBase : ViewModelBase, INavigatorAware, INavigationEventSupport
{
    public INavigator Navigator { get; set; } = default!;

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        System.Diagnostics.Debug.WriteLine($"{GetType()} is Disposed");
    }

    public virtual void OnNavigatingFrom(INavigationContext context)
    {
    }

    public virtual void OnNavigatingTo(INavigationContext context)
    {
    }

    public virtual void OnNavigatedTo(INavigationContext context)
    {
    }
}
