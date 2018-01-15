﻿namespace Smart.Navigation.Strategies
{
    using System;

    using Smart.Mock;

    using Xunit;

    public class ForwardStrategyTest
    {
        [Fact]
        public static void TestNavigatorForward()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockProvider()
                .ToNavigator();

            navigator.Register(Pages.Page1, typeof(Page1));
            navigator.Register(Pages.Page2, typeof(Page2));

            var context = new Holder<INavigationContext>();
            navigator.NavigatedTo += (sender, args) => { context.Value = args.Context; };

            // test
            navigator.Forward(Pages.Page1);

            Assert.Equal(1, navigator.StackedCount);
            var page1 = (MockPage)navigator.CurrentPage;
            Assert.Equal(typeof(Page1), page1.GetType());
            Assert.True(page1.IsOpen);

            Assert.Null(context.Value.FromId);
            Assert.Equal(Pages.Page1, context.Value.ToId);
            Assert.Equal(NavigationAttributes.None, context.Value.Attribute);

            navigator.Forward(Pages.Page2);

            Assert.Equal(1, navigator.StackedCount);
            var page2 = (MockPage)navigator.CurrentPage;
            Assert.Equal(typeof(Page2), page2.GetType());
            Assert.True(page2.IsOpen);
            Assert.False(page1.IsOpen);

            Assert.Equal(Pages.Page1, context.Value.FromId);
            Assert.Equal(Pages.Page2, context.Value.ToId);
            Assert.Equal(NavigationAttributes.None, context.Value.Attribute);
        }

        [Fact]
        public static void TestNavigatorForwardWithStacked()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockProvider()
                .ToNavigator();

            navigator.Register(Pages.Page1, typeof(Page1));
            navigator.Register(Pages.Page2, typeof(Page2));
            navigator.Register(Pages.Page3, typeof(Page3));

            // test
            navigator.Forward(Pages.Page1);
            navigator.Push(Pages.Page2);
            navigator.Forward(Pages.Page3);

            Assert.Equal(2, navigator.StackedCount);
            var page3 = (MockPage)navigator.CurrentPage;
            Assert.Equal(typeof(Page3), page3.GetType());
            Assert.True(page3.IsOpen);
        }

        [Fact]
        public static void TestNavigatorForwardFailed()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockProvider()
                .ToNavigator();

            navigator.Register(Pages.Page1, typeof(Page1));

            // test
            navigator.Forward(Pages.Page1);
            Assert.Throws<InvalidOperationException>(() => navigator.Push(Pages.Page2));
        }

        public enum Pages
        {
            Page1,
            Page2,
            Page3
        }

        public class Page1 : MockPage
        {
        }

        public class Page2 : MockPage
        {
        }

        public class Page3 : MockPage
        {
        }
    }
}
