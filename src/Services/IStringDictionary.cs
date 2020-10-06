namespace LostTech.Stack.Extensibility.Services {
    public interface IStringDictionary<T> {
        bool TryGet(string key, out T value);
    }
}
