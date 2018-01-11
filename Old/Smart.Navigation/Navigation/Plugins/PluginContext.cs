﻿namespace Smart.Navigation.Plugins
{
    using System;
    using System.Collections.Generic;

    using Smart.ComponentModel;

    public class PluginContext : IPluginContext
    {
        private Dictionary<Type, object> store;

        public IComponentContainer Components { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="components"></param>
        public PluginContext(IComponentContainer components)
        {
            Components = components;
        }

        private void Prepare()
        {
            if (store == null)
            {
                store = new Dictionary<Type, object>();
            }
        }

        public void Save<T>(Type type, T value)
        {
            Prepare();
            store[type] = value;
        }

        public T Load<T>(Type type)
        {
            Prepare();
            return (T)store[type];
        }

        public T LoadOr<T>(Type type, T defaultValue)
        {
            if (store == null)
            {
                return defaultValue;
            }

            Prepare();

            object value;
            return store.TryGetValue(type, out value) ? (T)value : defaultValue;
        }

        public T LoadOr<T>(Type type, Func<T> defaultValueFactory)
        {
            if (defaultValueFactory == null)
            {
                throw new ArgumentNullException(nameof(defaultValueFactory));
            }

            if (store == null)
            {
                return defaultValueFactory();
            }

            Prepare();

            object value;
            return store.TryGetValue(type, out value) ? (T)value : defaultValueFactory();
        }
    }
}