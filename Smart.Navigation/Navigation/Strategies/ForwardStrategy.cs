﻿namespace Smart.Navigation.Strategies
{
    using System;

    public class ForwardStrategy : INavigationStrategy
    {
        private readonly object id;

        public ForwardStrategy(object id)
        {
            this.id = id;
        }

        public void Process()
        {
            // TODO
            if (id == null)
            {
                throw new InvalidOperationException();
            }

            throw new System.NotImplementedException();
        }
    }
}