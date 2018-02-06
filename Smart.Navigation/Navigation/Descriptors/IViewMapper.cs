﻿namespace Smart.Navigation.Descriptors
{
    public interface IViewMapper
    {
        ViewDescriptor FindDescriptor(object id);

        void Updated(object id);
    }
}
