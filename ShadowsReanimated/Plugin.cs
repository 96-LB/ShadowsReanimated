using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Shared;
using System;
using System.Linq;

namespace ShadowsReanimated;


[BepInPlugin(GUID, NAME, VERSION)]
public class Plugin : BaseUnityPlugin {
    const string GUID = "com.lalabuff.necrodancer.shadowsreanimated";
    const string NAME = "ShadowsReanimated";
    const string VERSION = "0.1.0";
    readonly static string[] BUILDS = ["1.4.0-b20638"];

    internal static ManualLogSource Log { get; private set; }

    internal void Awake() {
        try {
            Log = Logger;

            var build = BuildInfoHelper.Instance.BuildId;
            if(!BUILDS.Contains(build)) {
                Log.LogFatal($"The current version of the game is not compatible with this plugin. Please update the game or the mod to the correct version. The current mod version is v{VERSION} and the current game version is {build}. Allowed game versions: {string.Join(", ", BUILDS)}");
                return;
            }
        
            ShadowsReanimated.Config.Initialize(Config);
            Assets.Initialize();

            Harmony harmony = new(GUID);
            harmony.PatchAll();
            foreach(var x in harmony.GetPatchedMethods()) {
                Log.LogInfo($"Patched {x}.");
            }
            Log.LogMessage($"{NAME} v{VERSION} ({GUID}) has been loaded!");
        } catch(Exception e) {
            Log.LogFatal("Encountered error while trying to initialize plugin.");
            Log.LogFatal(e);
        }
    }
}
