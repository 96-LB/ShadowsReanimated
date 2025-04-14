using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace ShadowsReanimated;


[BepInPlugin(GUID, NAME, VERSION)]
public class Plugin : BaseUnityPlugin {
    const string GUID = "com.lalabuff.necrodancer.shadowsreanimated";
    const string NAME = "ShadowsReanimated";
    const string VERSION = "0.0.1";

    internal static ManualLogSource Log { get; private set; }

    internal void Awake() {
        Log = Logger;
        
        ShadowsReanimated.Config.Initialize(Config);
        Assets.Initialize();

        Harmony harmony = new(GUID);
        harmony.PatchAll();
        foreach(var x in harmony.GetPatchedMethods()) {
            Log.LogInfo($"Patched {x}.");
        }
        Log.LogMessage($"{NAME} v{VERSION} ({GUID}) has been loaded!");
    }
}
