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
        public static ConfigEntry<ProfileType> Profile;
        
        public static void Initialize(ConfigGroup config) {
            Profile = config.Bind("Profile", ProfileType.Default, "Selects a custom shadow profile.");
        }
    }

    public static void Initialize(ConfigFile config) {
        General.Initialize(new(config, "General"));
    }
}
