﻿namespace Example.FormsApp.Modules.Stack
{
    using Smart.Forms.Input;
    using Smart.Navigation;

    public class Stack3ViewModel : AppViewModelBase
    {
        public AsyncCommand<int> Pop { get; }

        public Stack3ViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
            Pop = MakeAsyncCommand<int>(x => Navigator.PopAsync(x));
        }
    }
}
