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
        public static ConfigEntry<bool> Colors;
        public static ConfigEntry<bool> VibeChainOverride;
        public static ConfigEntry<bool> VibePowerOverride;

        public static void Initialize(ConfigGroup config) {
            Preset = config.Bind("Preset", PresetType.Default, "Select a custom shadow preset.");
            Colors = config.Bind("Custom Colors", true, "Enable custom colors for shadows.");
            VibeChainOverride = config.Bind("Vibe Chain Override", false, "Override the shadow color of enemies in a vibe chain.");
            VibePowerOverride = config.Bind("Vibe Power Override", false, "Override the shadow color of enemies when vibe power is active.");
        }
    }

    public static class Sprites {
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
        
        public static SpriteType GetSpriteType(BeatType beatType) => (beatType switch {
            BeatType.OnBeat => OnBeat,
            BeatType.SixthBeat => SixthBeat,
            BeatType.QuarterBeat => QuarterBeat,
            BeatType.ThirdBeat => ThirdBeat,
            BeatType.HalfBeat => HalfBeat,
            BeatType.TwoThirdBeat => TwoThirdBeat,
            BeatType.ThreeQuarterBeat => ThreeQuarterBeat,
            BeatType.FiveSixthBeat => FiveSixthBeat,
            _ => OtherBeat
        }).Value;
    }

    public static class Colors {
        public static ConfigEntry<Color> OnBeat;
        public static ConfigEntry<Color> SixthBeat;
        public static ConfigEntry<Color> QuarterBeat;
        public static ConfigEntry<Color> ThirdBeat;
        public static ConfigEntry<Color> HalfBeat;
        public static ConfigEntry<Color> TwoThirdBeat;
        public static ConfigEntry<Color> ThreeQuarterBeat;
        public static ConfigEntry<Color> FiveSixthBeat;
        public static ConfigEntry<Color> OtherBeat;

        public static void Initialize(ConfigGroup config) {
            var purple = new Color(0.6f, 0.5f, 1f);
            var red = new Color(1f, 0.4f, 0.5f);
            var yellow = new Color(1f, 0.9f, 0.6f);
            var green = new Color(0.5f, 1f, 0.6f);
            var blue = new Color(0.3f, 0.7f, 1f);
            OnBeat = config.Bind("On Beat", purple, "Color for the shadows of monsters aligned to the beat.");
            SixthBeat = config.Bind("Sixth Beat", red, "Color for the shadows of monsters aligned to the sixth-beat.");
            QuarterBeat = config.Bind("Quarter Beat", yellow, "Color for the shadows of monsters aligned to the quarter-beat.");
            ThirdBeat = config.Bind("Third Beat", green, "Color for the shadows of monsters aligned to the third-beat.");
            HalfBeat = config.Bind("Half Beat", blue, "Color for the shadows of monsters aligned to the half-beat.");
            TwoThirdBeat = config.Bind("Two-Third Beat", green, "Color for the shadows of monsters aligned to the two-third-beat.");
            ThreeQuarterBeat = config.Bind("Three-Quarter Beat", yellow, "Color for the shadows of monsters aligned to the three-quarter-beat.");
            FiveSixthBeat = config.Bind("Five-Sixth Beat", red, "Color for the shadows of monsters aligned to the five-sixth-beat.");
            OtherBeat = config.Bind("Other", red, "Color for the shadow of all other monsters.");
        }

        public static Color GetColor(BeatType beatType) => (beatType switch {
            BeatType.OnBeat => OnBeat,
            BeatType.SixthBeat => SixthBeat,
            BeatType.QuarterBeat => QuarterBeat,
            BeatType.ThirdBeat => ThirdBeat,
            BeatType.HalfBeat => HalfBeat,
            BeatType.TwoThirdBeat => TwoThirdBeat,
            BeatType.ThreeQuarterBeat => ThreeQuarterBeat,
            BeatType.FiveSixthBeat => FiveSixthBeat,
            _ => OtherBeat
        }).Value;
    }

    public static class VersionControl {
        public static ConfigEntry<string> VersionOverride;

        public static void Initialize(ConfigGroup config) {
            VersionOverride = config.Bind("Version Override", "", "Input the current build version or '*' to override the version check.");
        }
    }

    public static void Initialize(ConfigFile config) {
        General.Initialize(new(config, "General"));
        Sprites.Initialize(new(config, "Custom Sprites"));
        Colors.Initialize(new(config, "Custom Colors"));
        VersionControl.Initialize(new(config, "Version Control"));
    }
}
