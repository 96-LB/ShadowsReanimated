﻿using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Shared;
using System;
using System.Linq;

namespace ShadowsReanimated;


[BepInPlugin(GUID, NAME, VERSION)]
public class Plugin : BaseUnityPlugin {
    public const string GUID = "com.lalabuff.necrodancer.shadowsreanimated";
    public const string NAME = "ShadowsReanimated";
    public const string VERSION = "0.2.5";

    public const string ALLOWED_VERSIONS = "1.7.0 1.6.0 1.5.1 1.5.0 1.4.0";
    public static string[] AllowedVersions => ALLOWED_VERSIONS.Split(' ');

    internal static ManualLogSource Log { get; private set; }

    internal void Awake() {
        try {
            Log = Logger;

            ShadowsReanimated.Config.Initialize(Config);

            var gameVersion = BuildInfoHelper.Instance.BuildId.Split('-')[0];
            var overrideVersion = ShadowsReanimated.Config.VersionControl.VersionOverride.Value;
            var check = AllowedVersions.Contains(gameVersion) || gameVersion == overrideVersion || overrideVersion == "*";
            if(!check) {
                Log.LogFatal($"The current version of the game is not compatible with this plugin. Please update the game or the mod to the correct version. The current mod version is v{VERSION} and the current game version is {gameVersion}. Allowed game versions: {string.Join(", ", AllowedVersions)}");
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
