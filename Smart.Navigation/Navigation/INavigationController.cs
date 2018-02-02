﻿namespace Smart.Navigation
{
    using System;

    using System.Collections.Generic;

    public interface INavigationController
    {
        IViewMapper ViewMapper { get; }

        List<ViewStackInfo> ViewStack { get; }

        object CreateView(Type type);

        void OpenView(object view);

        void CloseView(object view);

        void ActiveView(object view, object parameter);

        object DeactiveView(object view);
    }
}
