﻿namespace Smart.Navigation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Smart.ComponentModel;
    using Smart.Functional;
    using Smart.Navigation.Plugins;
    using Smart.Navigation.Strategies;

    /// <summary>
    ///
    /// </summary>
    public class Navigator : DisposableObject, INavigator
    {
        // ------------------------------------------------------------
        // Event
        // ------------------------------------------------------------

        public event EventHandler<ConfirmEventArgs> Confirm;

        public event EventHandler<NavigationEventArgs> NavigatedFrom;

        public event EventHandler<NavigationEventArgs> NavigatedTo;

        public event EventHandler<EventArgs> Exited;

        // ------------------------------------------------------------
        // Member
        // ------------------------------------------------------------

        private static readonly NavigationParameter EmptyParameter = new NavigationParameter();

        private readonly ComponentContainer components;

        private readonly Dictionary<object, PageDescriptor> descriptors = new Dictionary<object, PageDescriptor>();

        private readonly PageStackManager stackManager = new PageStackManager();

        private readonly INavigationProvider provider;

        private readonly IPlugin[] plugins;

        // ------------------------------------------------------------
        // Property
        // ------------------------------------------------------------

        public int StackedCount => stackManager.Stacked.Count;

        public object CurrentPageId => stackManager.CurrentPageId;

        public object CurrentPage => stackManager.CurrentPage;

        public object CurrentTarget => stackManager.CurrentPage.MapOrDefalut(x => provider.ResolveTarget(x));

        // ------------------------------------------------------------
        // Constructor
        // ------------------------------------------------------------

        public Navigator()
            : this(new NavigatorConfig())
        {
        }

        public Navigator(INavigatorConfig config)
        {
            components = config.ResolveComponents();

            provider = components.Get<INavigationProvider>();
            plugins = components.GetAll<IPlugin>().ToArray();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        // ------------------------------------------------------------
        // Registration
        // ------------------------------------------------------------

        public void Register(object id, Type type)
        {
            descriptors.Add(id, new PageDescriptor(id, type));
        }

        // ------------------------------------------------------------
        // Navigation
        // ------------------------------------------------------------

        public void Exit()
        {
            for (var i = stackManager.Stacked.Count - 1; i >= 0; i--)
            {
                var page = stackManager.Stacked[i].Page;
                var target = provider.ResolveTarget(page);

                provider.ClosePage(page);

                (page as IDisposable)?.Dispose();
                if (page != target)
                {
                    (target as IDisposable)?.Dispose();
                }
            }

            stackManager.Stacked.Clear();

            Exited?.Invoke(this, EventArgs.Empty);
        }

        public bool Navigate(INavigationStrategy strategy, INavigationParameter parameter)
        {
            // TODO controller
            var result = strategy.Initialize(null);
            var context = new NavigationContext(CurrentPageId, result.ToId, result.Attribute, parameter ?? EmptyParameter);

            if (!ConfirmNavigation(context))
            {
                return false;
            }

            // TODO

            throw new System.NotImplementedException();
        }

        // TODO async?

        // ------------------------------------------------------------
        // Helper
        // ------------------------------------------------------------

        private bool ConfirmNavigation(NavigationContext context)
        {
            var page = CurrentPage;
            if (page != null)
            {
                var target = provider.ResolveTarget(page);
                if (target is IConfirmRequest confirm)
                {
                    var cancel = confirm.NavigationConfirm(context);
                    if (cancel)
                    {
                        return false;
                    }
                }
            }

            var handler = Confirm;
            if (handler != null)
            {
                var args = new ConfirmEventArgs(context);
                handler(this, args);
                if (args.Cancel)
                {
                    return false;
                }
            }

            return true;
        }
    }
}