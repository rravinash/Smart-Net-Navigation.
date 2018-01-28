﻿namespace Example.WindowsFormsApp.Views.Data
{
    using System.Linq;

    using Example.WindowsFormsApp.Models;
    using Example.WindowsFormsApp.Services;

    using Smart.Navigation;
    using Smart.Resolver.Attributes;

    [View(ViewId.DataList)]
    public partial class DataListView : AppViewBase
    {
        public override string Title => "Data List";

        public override bool CanGoHome => true;

        [Inject]
        public DataService DataService { get; set; }

        public DataListView()
        {
            InitializeComponent();
        }

        public override void OnNavigatingTo(INavigationContext context)
        {
            DataListBox.DataSource = DataService.QueryDataList().ToList();
        }

        public override void OnGoHome()
        {
            Navigator.Forward(ViewId.Menu);
        }

        private void OnNewButtonClick(object sender, System.EventArgs e)
        {
            Navigator.Forward(ViewId.DataDetailNew);
        }

        private void OnEditButtonClick(object sender, System.EventArgs e)
        {
            var parameter = new NavigationParameter();
            parameter.SetValue((DataEntity)DataListBox.SelectedItem);
            Navigator.Forward(ViewId.DataDetailEdit, parameter);
        }
    }
}