namespace LostTech.Stack.Extensibility.Actions {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using LostTech.Stack.Extensibility.Filters;
    using LostTech.Stack.Extensibility.Services;
    using LostTech.Stack.WindowManagement;

    using Microsoft.Xaml.Behaviors;

    public class MoveHere : TriggerAction<ContentControl> {
        readonly IWindowManager windowManager;
        readonly IEnumerable<IAppWindow> allWindows;
        readonly IStringDictionary<IFilter<IAppWindow>> windowFilters;

        public MoveHere() : this((IServiceProvider)Application.Current) { }
        public MoveHere(IServiceProvider serviceProvider) {
            this.allWindows = serviceProvider.Get<IEnumerable<IAppWindow>>();
            this.windowManager = serviceProvider.Get<IWindowManager>();
            this.windowFilters = serviceProvider.Get<IStringDictionary<IFilter<IAppWindow>>>();
        }

        public string? GroupName {
            get => (string?)this.GetValue(GroupNameProperty);
            set => this.SetValue(GroupNameProperty, value);
        }
        public static DependencyProperty GroupNameProperty =
            DependencyProperty.Register(nameof(GroupName), typeof(string),
                ownerType: typeof(MoveHere));

        protected override void Invoke(object? parameter) {
            if (!(this.AssociatedObject is IZone target))
                return;
            if (this.GroupName is null)
                return;

            if (!this.windowFilters.TryGet(this.GroupName, out var filter)
                || filter is null)
                return;

            foreach (var window in this.allWindows) {
                try {
                    if (!window.IsVisibleInAppSwitcher)
                        return;

                    if (!filter.Matches(window))
                        return;

                    this.windowManager.Move(window, target).Wait();
                } catch (WindowNotFoundException) { }
            }
        }
    }
}
