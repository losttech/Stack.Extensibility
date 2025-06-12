namespace LostTech.Stack.Extensibility.Filters;

public sealed class FilterWrapper: IFilter<IntPtr> {
    public bool Matches(nint value) => this.Filter?.Matches(value) == true;
    public IFilter<IntPtr>? Filter { get; set; }
}