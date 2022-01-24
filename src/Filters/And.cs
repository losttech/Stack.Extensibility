using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace LostTech.Stack.Extensibility.Filters;

public sealed class And : IFilter<IntPtr> {
    public ObservableCollection<IFilter<IntPtr>> Filters { get; } = new();
    public bool Matches(IntPtr value) => this.Filters.All(f => f.Matches(value));
}
