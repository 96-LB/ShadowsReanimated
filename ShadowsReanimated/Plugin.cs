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
    const string VERSION = "0.2.2";
    readonly static string[] BUILDS = ["1.5.0", "1.4.0"];

    internal static ManualLogSource Log { get; private set; }

    internal void Awake() {
        try {
            Log = Logger;

            ShadowsReanimated.Config.Initialize(Config);

            var build = BuildInfoHelper.Instance.BuildId.Split('-')[0];
            var overrideVersion = ShadowsReanimated.Config.VersionControl.VersionOverride.Value;
            var check = BUILDS.Contains(build) || build == overrideVersion || overrideVersion == "*";
            if(!check) {
                Log.LogFatal($"The current version of the game is not compatible with this plugin. Please update the game or the mod to the correct version. The current mod version is v{VERSION} and the current game version is {build}. Allowed game versions: {string.Join(", ", BUILDS)}");
                return;
            }
            
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
