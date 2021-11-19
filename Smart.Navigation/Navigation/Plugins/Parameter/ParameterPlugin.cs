namespace Smart.Navigation.Plugins.Parameter;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Smart.Navigation.Components;
using Smart.Reflection;

public class ParameterPlugin : PluginBase
{
    private readonly Dictionary<Type, ParameterProperty[]> typeProperties = new();

    private readonly IDelegateFactory delegateFactory;

    private readonly IConverter converter;

    public ParameterPlugin(IDelegateFactory delegateFactory, IConverter converter)
    {
        this.delegateFactory = delegateFactory;
        this.converter = converter;
    }

    private ParameterProperty[] GetTypeProperties(Type type)
    {
        if (!typeProperties.TryGetValue(type, out var properties))
        {
            properties = type.GetProperties()
                .Select(x => new
                {
                    Property = x,
                    Attribute = (ParameterAttribute?)x.GetCustomAttribute(typeof(ParameterAttribute))
                })
                .Where(x => x.Attribute is not null)
                .Select(x => new ParameterProperty(
                    x.Attribute!.Name ?? x.Property.Name,
                    delegateFactory.GetExtendedPropertyType(x.Property),
                    (x.Attribute!.Direction & Directions.Export) != 0 ? delegateFactory.CreateGetter(x.Property, true) : null,
                    (x.Attribute!.Direction & Directions.Import) != 0 ? delegateFactory.CreateSetter(x.Property, true) : null))
                .ToArray();
            typeProperties[type] = properties;
        }

        return properties;
    }

    public override void OnNavigatingFrom(IPluginContext pluginContext, INavigationContext navigationContext, object? view, object? target)
    {
        if (target is null)
        {
            return;
        }

        pluginContext.Save(GetType(), GatherExportParameters(target));
    }

    public override void OnNavigatingTo(IPluginContext pluginContext, INavigationContext navigationContext, object view, object target)
    {
        var parameters = pluginContext.LoadOr(GetType(), default(Dictionary<string, object?>));
        if (parameters is null)
        {
            return;
        }

        ApplyImportParameters(target, parameters);
    }

    private Dictionary<string, object?> GatherExportParameters(object target)
    {
        var parameters = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);

        foreach (var property in GetTypeProperties(target.GetType()))
        {
            if (property.Getter is not null)
            {
                parameters.Add(property.Name, property.Getter(target));
            }
        }

        return parameters;
    }

    private void ApplyImportParameters(object target, IDictionary<string, object?> parameters)
    {
        foreach (var property in GetTypeProperties(target.GetType()))
        {
            if ((property.Setter is not null) &&
                parameters.TryGetValue(property.Name, out var value))
            {
                property.Setter(target, converter.Convert(value, property.PropertyType));
            }
        }
    }
}
