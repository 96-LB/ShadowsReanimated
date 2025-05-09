using BepInEx.Configuration;
using UnityEngine;
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
        public static ConfigEntry<PresetType> Preset;
        
        public static void Initialize(ConfigGroup config) {
            Preset = config.Bind("Preset", PresetType.Default, "Selects a custom shadow preset.");
        }
    }

    public static class Custom {
        public static ConfigEntry<SpriteType> OnBeat;
        public static ConfigEntry<SpriteType> SixthBeat;
        public static ConfigEntry<SpriteType> QuarterBeat;
        public static ConfigEntry<SpriteType> ThirdBeat;
        public static ConfigEntry<SpriteType> HalfBeat;
        public static ConfigEntry<SpriteType> TwoThirdBeat;
        public static ConfigEntry<SpriteType> ThreeQuarterBeat;
        public static ConfigEntry<SpriteType> FiveSixthBeat;
        public static ConfigEntry<SpriteType> OtherBeat;

        public static void Initialize(ConfigGroup config) {
            OnBeat = config.Bind("On Beat", SpriteType.Circle, "Sprite for the shadows of monsters aligned to the beat.");
            SixthBeat = config.Bind("Sixth Beat", SpriteType.Star, "Sprite for the shadows of monsters aligned to the sixth-beat.");
            QuarterBeat = config.Bind("Quarter Beat", SpriteType.LeftTriangle, "Sprite for the shadows of monsters aligned to the quarter-beat.");
            ThirdBeat = config.Bind("Third Beat", SpriteType.LeftTrapezoid, "Sprite for the shadows of monsters aligned to the third-beat.");
            HalfBeat = config.Bind("Half Beat", SpriteType.Diamond, "Sprite for the shadows of monsters aligned to the half-beat.");
            TwoThirdBeat = config.Bind("Two-Third Beat", SpriteType.RightTrapezoid, "Sprite for the shadows of monsters aligned to the two-third-beat.");
            ThreeQuarterBeat = config.Bind("Three-Quarter Beat", SpriteType.RightTriangle, "Sprite for the shadows of monsters aligned to the three-quarter-beat.");
            FiveSixthBeat = config.Bind("Five-Sixth Beat", SpriteType.Star, "Sprite for the shadows of monsters aligned to the five-sixth-beat.");
            OtherBeat = config.Bind("Other", SpriteType.Star, "Sprite for the shadow of all other monsters.");
        }
    }

    public static void Initialize(ConfigFile config) {
        General.Initialize(new(config, "General"));
        Custom.Initialize(new(config, "Custom"));
    }
}
