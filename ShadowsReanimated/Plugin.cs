using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace ShadowsReanimated;


[BepInPlugin(GUID, NAME, VERSION)]
public class Plugin : BaseUnityPlugin {
    const string GUID = "com.lalabuff.necrodancer.shadowsreanimated";
    const string NAME = "ShadowsReanimated";
    const string VERSION = "0.0.1";

    internal static ManualLogSource Log;

    internal static Sprite QuarterBeat;
    internal static Sprite ThirdBeat;
    internal static Sprite TwoThirdBeat;
    internal static Sprite ThreeQuarterBeat;

    private Sprite MakeSprite(byte[] data) {
        Texture2D tex = new(0, 0);
        tex.LoadImage(data);
        return Sprite.Create(
            tex,
            new(0, 0, tex.width, tex.height),
            new(0.5f, 0.5f),
            48
        );
    }

    internal void Awake() {
        Log = Logger;

        Harmony harmony = new(GUID);
        harmony.PatchAll();
        
        ShadowsReanimated.Config.Initialize(Config);

        QuarterBeat = MakeSprite(Properties.Resources.QuarterBeat);
        ThirdBeat = MakeSprite(Properties.Resources.ThirdBeat);
        TwoThirdBeat = MakeSprite(Properties.Resources.TwoThirdBeat);
        ThreeQuarterBeat = MakeSprite(Properties.Resources.ThreeQuarterBeat);

        Log.LogInfo($"{NAME} v{VERSION} ({GUID}) has been loaded! Have fun!");
        foreach(var x in harmony.GetPatchedMethods()) {
            Log.LogInfo($"Patched {x}.");
        }
    }
}
