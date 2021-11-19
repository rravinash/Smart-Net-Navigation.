namespace Smart.Navigation.Components;

using System;

public sealed class DelegateServiceProvider : IServiceProvider
{
    private readonly Func<Type, object?> callback;

    public DelegateServiceProvider(Func<Type, object?> callback)
    {
        this.callback = callback;
    }

    public object? GetService(Type serviceType) => callback(serviceType);
}
