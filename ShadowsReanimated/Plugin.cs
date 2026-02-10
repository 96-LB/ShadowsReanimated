using BepInEx;
using RiftOfTheNecroManager;

namespace ShadowsReanimated;


[BepInPlugin(GUID, NAME, VERSION)]
[NecroManagerInfo(isBeta: true)]
public class Plugin : RiftPlugin {
    public const string GUID = "com.lalabuff.necrodancer.shadowsreanimated";
    public const string NAME = "ShadowsReanimated";
    public const string VERSION = "1.0.0";
}
