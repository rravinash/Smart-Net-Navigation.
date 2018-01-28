﻿namespace Example.WindowsFormsApp.Views.Stack
{
    using Smart.Navigation;

    [View(ViewId.Stack1)]
    public partial class Stack1View : AppViewBase
    {
        public override string Title => "Stack1View";

        public override bool CanGoHome => true;

        public Stack1View()
        {
            InitializeComponent();
        }

        public override void OnGoHome()
        {
            Navigator.Forward(ViewId.Menu);
        }

        private void OnCancelButtonClick(object sender, System.EventArgs e)
        {
            Navigator.Forward(ViewId.Menu);
        }

        private void OnPushButtonClick(object sender, System.EventArgs e)
        {
            Navigator.Push(ViewId.Stack2);
        }
    }
}