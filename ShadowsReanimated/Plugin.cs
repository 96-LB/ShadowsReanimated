using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using RiftOfTheNecroManager;
using Shared;
using System;

namespace ShadowsReanimated;


[BepInPlugin("com.lalabuff.necrodancer.shadowsreanimated", "ShadowsReanimated", "0.2.8")]
public class Plugin : RiftPlugin {
    public override string AllowedVersions => "1.10.0";
}
