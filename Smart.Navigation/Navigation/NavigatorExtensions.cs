namespace Smart.Navigation
{
    using System.Threading.Tasks;

    using Smart.Navigation.Strategies;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extension method")]
    public static class NavigatorExtensions
    {
        // ------------------------------------------------------------
        // Notify
        // ------------------------------------------------------------

        public static void Notify<T>(this INavigator navigator, T parameter)
        {
            if (navigator.CurrentTarget is INotifySupport<T> notifySupport)
            {
                notifySupport.NavigatorNotify(parameter);
            }
            else
            {
                (navigator.CurrentTarget as INotifySupport)?.NavigatorNotify(parameter);
            }
        }

        public static ValueTask NotifyAsync<T>(this INavigator navigator, T parameter)
        {
            if (navigator.CurrentTarget is INotifySupportAsync<T> notifySupportAsyncT)
            {
                return notifySupportAsyncT.NavigatorNotifyAsync(parameter);
            }

            if (navigator.CurrentTarget is INotifySupportAsync notifySupportAsync)
            {
                return notifySupportAsync.NavigatorNotifyAsync(parameter);
            }

            if (navigator.CurrentTarget is INotifySupport<T> notifySupport)
            {
                notifySupport.NavigatorNotify(parameter);
                return default;
            }

            (navigator.CurrentTarget as INotifySupport)?.NavigatorNotify(parameter);
            return default;
        }

        // ------------------------------------------------------------
        // Forward
        // ------------------------------------------------------------

        public static bool Forward(this INavigator navigator, object id)
        {
            return navigator.Navigate(new ForwardStrategy(id), null);
        }

        public static bool Forward(this INavigator navigator, object id, INavigationParameter parameter)
        {
            return navigator.Navigate(new ForwardStrategy(id), parameter);
        }

        // Async

        public static ValueTask<bool> ForwardAsync(this INavigator navigator, object id)
        {
            return navigator.NavigateAsync(new ForwardStrategy(id), null);
        }

        public static ValueTask<bool> ForwardAsync(this INavigator navigator, object id, INavigationParameter parameter)
        {
            return navigator.NavigateAsync(new ForwardStrategy(id), parameter);
        }

        // ------------------------------------------------------------
        // Push
        // ------------------------------------------------------------

        public static bool Push(this INavigator navigator, object id)
        {
            return navigator.Navigate(new PushStrategy(id), null);
        }

        public static bool Push(this INavigator navigator, object id, INavigationParameter parameter)
        {
            return navigator.Navigate(new PushStrategy(id), parameter);
        }

        // Async

        public static ValueTask<bool> PushAsync(this INavigator navigator, object id)
        {
            return navigator.NavigateAsync(new PushStrategy(id), null);
        }

        public static ValueTask<bool> PushAsync(this INavigator navigator, object id, INavigationParameter parameter)
        {
            return navigator.NavigateAsync(new PushStrategy(id), parameter);
        }

        // ------------------------------------------------------------
        // Pop
        // ------------------------------------------------------------

        public static bool Pop(this INavigator navigator)
        {
            return navigator.Navigate(new PopStrategy(1), null);
        }

        public static bool Pop(this INavigator navigator, INavigationParameter parameter)
        {
            return navigator.Navigate(new PopStrategy(1), parameter);
        }

        public static bool Pop(this INavigator navigator, int level)
        {
            return navigator.Navigate(new PopStrategy(level), null);
        }

        public static bool Pop(this INavigator navigator, int level, INavigationParameter parameter)
        {
            return navigator.Navigate(new PopStrategy(level), parameter);
        }

        // Async

        public static ValueTask<bool> PopAsync(this INavigator navigator)
        {
            return navigator.NavigateAsync(new PopStrategy(1), null);
        }

        public static ValueTask<bool> PopAsync(this INavigator navigator, INavigationParameter parameter)
        {
            return navigator.NavigateAsync(new PopStrategy(1), parameter);
        }

        public static ValueTask<bool> PopAsync(this INavigator navigator, int level)
        {
            return navigator.NavigateAsync(new PopStrategy(level), null);
        }

        public static ValueTask<bool> PopAsync(this INavigator navigator, int level, INavigationParameter parameter)
        {
            return navigator.NavigateAsync(new PopStrategy(level), parameter);
        }

        // ------------------------------------------------------------
        // PopAndForward
        // ------------------------------------------------------------

        public static bool PopAndForward(this INavigator navigator, object id)
        {
            return navigator.Navigate(new PopAndForwardStrategy(id, 1), null);
        }

        public static bool PopAndForward(this INavigator navigator, object id, int level)
        {
            return navigator.Navigate(new PopAndForwardStrategy(id, level), null);
        }

        public static bool PopAllAndForward(this INavigator navigator, object id)
        {
            return navigator.Navigate(new PopAndForwardStrategy(id), null);
        }

        public static bool PopAndForward(this INavigator navigator, object id, INavigationParameter parameter)
        {
            return navigator.Navigate(new PopAndForwardStrategy(id, 1), parameter);
        }

        public static bool PopAndForward(this INavigator navigator, object id, int level, INavigationParameter parameter)
        {
            return navigator.Navigate(new PopAndForwardStrategy(id, level), parameter);
        }

        public static bool PopAllAndForward(this INavigator navigator, object id, INavigationParameter parameter)
        {
            return navigator.Navigate(new PopAndForwardStrategy(id), parameter);
        }

        // Async

        public static ValueTask<bool> PopAndForwardAsync(this INavigator navigator, object id)
        {
            return navigator.NavigateAsync(new PopAndForwardStrategy(id, 1), null);
        }

        public static ValueTask<bool> PopAndForwardAsync(this INavigator navigator, object id, int level)
        {
            return navigator.NavigateAsync(new PopAndForwardStrategy(id, level), null);
        }

        public static ValueTask<bool> PopAllAndForwardAsync(this INavigator navigator, object id)
        {
            return navigator.NavigateAsync(new PopAndForwardStrategy(id), null);
        }

        public static ValueTask<bool> PopAndForwardAsync(this INavigator navigator, object id, INavigationParameter parameter)
        {
            return navigator.NavigateAsync(new PopAndForwardStrategy(id, 1), parameter);
        }

        public static ValueTask<bool> PopAndForwardAsync(this INavigator navigator, object id, int level, INavigationParameter parameter)
        {
            return navigator.NavigateAsync(new PopAndForwardStrategy(id, level), parameter);
        }

        public static ValueTask<bool> PopAllAndForwardAsync(this INavigator navigator, object id, INavigationParameter parameter)
        {
            return navigator.NavigateAsync(new PopAndForwardStrategy(id), parameter);
        }
    }
}
