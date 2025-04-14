using BepInEx.Configuration;

namespace ShadowsReanimated;

public static class Config {
    public class ConfigGroup(ConfigFile config, string group) {
        public ConfigEntry<T> Bind<T>(string key, T defaultValue, string description) {
            return config.Bind(group, key, defaultValue, description);
        }

        public ConfigEntry<T> Bind<T>(string key, T defaultValue, ConfigDescription description = null) {
            return config.Bind(group, key, defaultValue, description);
        }
    }

    public static class MoreShadows {
        public static bool Enabled => enabled.Value;
        private static ConfigEntry<bool> enabled;

        public static void Initialize(ConfigGroup config) {
            enabled = config.Bind("Enabled", true, "Enable more shadow shapes for off-beat enemies.");
        }
    }

    public static class SfxPrediction {
        public static bool Enabled => enabled.Value;
        private static ConfigEntry<bool> enabled;
        public static void Initialize(ConfigGroup config) {
            enabled = config.Bind("Enabled", true, "Enable better sound effect prediction for enemy hits.");
        }
    }

    public static void Initialize(ConfigFile config) {
        MoreShadows.Initialize(new(config, "More Shadows"));
        SfxPrediction.Initialize(new(config, "SFX Prediction"));
    }
}
