namespace LostTech.Stack.Extensibility.Filters;

static class FilterExtensions {
    public static T From<T>(this T self, CommonStringMatchFilter filter) 
        where T: CommonStringMatchFilter {
        filter.CopyTo(self);
        return self;
    }
}
