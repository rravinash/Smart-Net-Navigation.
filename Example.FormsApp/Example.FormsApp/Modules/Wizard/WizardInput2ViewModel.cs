namespace Example.FormsApp.Modules.Wizard
{
    using System.Threading.Tasks;

    using Smart.ComponentModel;
    using Smart.Forms.Input;
    using Smart.Navigation;
    using Smart.Navigation.Plugins.Scope;

    public class WizardInput2ViewModel : AppViewModelBase
    {
        public static WizardInput2ViewModel DesignInstance { get; } = null; // For design

        [Scope]
        public NotificationValue<WizardContext> Context { get; } = new NotificationValue<WizardContext>();

        public AsyncCommand<ViewId> ForwardCommand { get; }

        public WizardInput2ViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
            ForwardCommand = MakeAsyncCommand<ViewId>(x => Navigator.ForwardAsync(x));
        }

        protected override Task OnNotifyFunction1Async()
        {
            return Navigator.ForwardAsync(ViewId.WizardInput1);
        }

        protected override Task OnNotifyFunction4Async()
        {
            return Navigator.ForwardAsync(ViewId.WizardResult);
        }
    }
}