namespace Smart.Navigation.Plugins.Resolver
{
    using System;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class PageContextAttribute : Attribute
    {
        public string Name { get; }

        public PageContextAttribute(string name)
        {
            Name = name;
        }
    }
}
