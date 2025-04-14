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

    public static class General {
        public static bool Enabled => enabled.Value;
        private static ConfigEntry<bool> enabled;

        public static void Initialize(ConfigGroup config) {
            enabled = config.Bind("Enabled", true, "Enables the mod.");
        }
    }

    public static void Initialize(ConfigFile config) {
        General.Initialize(new(config, "General"));
    }
}
