namespace LostTech.Stack.Extensibility.Services {
    using System.Threading.Tasks;

    using LostTech.Stack.WindowManagement;

    public interface IWindowManager {
        Task<bool?> Detach(IAppWindow window, bool restoreBounds = false);
        Task Move(IAppWindow window, IZone target);
    }
}
