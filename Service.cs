using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using Dalamud.Game;
using Dalamud.Game.ClientState.Keys;
namespace PriceInsight;

#pragma warning disable 8618
// ReSharper disable UnusedAutoPropertyAccessor.Local
internal class Service {
    [PluginService] internal static ICommandManager CommandManager { get; private set; }
    [PluginService] internal static IClientState ClientState { get; private set; }
    [PluginService] internal static IDataManager DataManager { get; private set; }
    [PluginService] internal static Framework Framework { get; private set; }
    [PluginService] internal static IGameGui GameGui { get; private set; }
    [PluginService] internal static KeyState KeyState { get; private set; }
    [PluginService] internal static IPluginLog PluginLog { get; private set; }
    [PluginService] public static IAddonLifecycle AddonLifecycle { get; private set; } = null!;

    internal static void Initialize(DalamudPluginInterface pluginInterface) {
        pluginInterface.Create<Service>();
    }
}
#pragma warning restore 8618
