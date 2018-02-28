﻿namespace Example.FormsApp.Modules.Wizard
{
    using Smart.ComponentModel;
    using Smart.Forms.Input;
    using Smart.Navigation;
    using Smart.Navigation.Plugins.Scope;

    public class WizardInput2ViewModel : AppViewModelBase
    {
        [Scope]
        public NotificationValue<WizardContext> Context { get; } = new NotificationValue<WizardContext>();

        public AsyncCommand<ViewId> Forward { get; }

        public WizardInput2ViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
            Forward = MakeAsyncCommand<ViewId>(x => Navigator.ForwardAsync(x));
        }
    }
}
