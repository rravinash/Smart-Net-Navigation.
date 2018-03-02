﻿namespace Example.FormsApp
{
    using Example.FormsApp.Modules;
    using Example.FormsApp.Views;

    using Smart.ComponentModel;
    using Smart.Forms.Components;
    using Smart.Forms.Input;
    using Smart.Forms.ViewModels;
    using Smart.Navigation;

    public class MainPageViewModel : ViewModelBase, IShellControl
    {
        public NotificationValue<string> Title { get; } = new NotificationValue<string>();

        public NotificationValue<bool> CanGoHome { get; } = new NotificationValue<bool>();

        public NotificationValue<string> Function1Text { get; } = new NotificationValue<string>();

        public NotificationValue<string> Function2Text { get; } = new NotificationValue<string>();

        public NotificationValue<string> Function3Text { get; } = new NotificationValue<string>();

        public NotificationValue<string> Function4Text { get; } = new NotificationValue<string>();

        public NotificationValue<bool> Function1Enabled { get; } = new NotificationValue<bool>();

        public NotificationValue<bool> Function2Enabled { get; } = new NotificationValue<bool>();

        public NotificationValue<bool> Function3Enabled { get; } = new NotificationValue<bool>();

        public NotificationValue<bool> Function4Enabled { get; } = new NotificationValue<bool>();

        public ApplicationState ApplicationState { get; }

        public INavigator Navigator { get; }

        public AsyncCommand GoHomeCommand { get; }

        public AsyncCommand OptionCommand { get; }

        public AsyncCommand Function1Command { get; }

        public AsyncCommand Function2Command { get; }

        public AsyncCommand Function3Command { get; }

        public AsyncCommand Function4Command { get; }

        public MainPageViewModel(
            ApplicationState applicationState,
            INavigator navigator,
            IDialogService dialogService)
            : base(applicationState)
        {
            ApplicationState = applicationState;
            Navigator = navigator;

            GoHomeCommand = MakeAsyncCommand(
                    () => Navigator.PopAllAndForwardAsync(ViewId.Menu),
                    () => CanGoHome.Value)
                .Observe(CanGoHome);
            OptionCommand = MakeAsyncCommand(() => dialogService.DisplayAlert("Option", "Option", "OK"));
            Function1Command = MakeAsyncCommand(
                    () => Navigator.NotifyAsync(FunctionKeys.Function1),
                    () => Function1Enabled.Value)
                .Observe(Function1Enabled);
            Function2Command = MakeAsyncCommand(
                    () => Navigator.NotifyAsync(FunctionKeys.Function2),
                    () => Function2Enabled.Value)
                .Observe(Function2Enabled);
            Function3Command = MakeAsyncCommand(
                    () => Navigator.NotifyAsync(FunctionKeys.Function3),
                    () => Function3Enabled.Value)
                .Observe(Function3Enabled);
            Function4Command = MakeAsyncCommand(
                    () => Navigator.NotifyAsync(FunctionKeys.Function4),
                    () => Function4Enabled.Value)
                .Observe(Function4Enabled);
        }
    }
}
