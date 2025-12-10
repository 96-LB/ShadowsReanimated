using RiftOfTheNecroManager;
using UnityEngine;

namespace ShadowsReanimated;


public static class Config {
    public static class General {
        public const string GROUP = "General";
        public static Setting<PresetType> Preset { get; }  = new(GROUP, "Preset", PresetType.Default, "Select a custom shadow preset.");  
        public static Setting<bool> Colors { get; } = new(GROUP, "Custom Colors", true, "Enable custom colors for shadows.");
        public static Setting<bool> VibeChainOverride { get; } = new(GROUP, "Vibe Chain Override", false, "Override the shadow color of enemies in a vibe chain.");
        public static Setting<bool> VibePowerOverride { get; } = new(GROUP, "Vibe Power Override", false, "Override the shadow color of enemies when vibe power is active.");
    }

    public static class Sprites {
        public const string GROUP = "Custom Sprites";
        public static Setting<SpriteType> OnBeat { get; } = new(GROUP, "On Beat", SpriteType.Circle, "Sprite for the shadows of monsters aligned to the beat.");
        public static Setting<SpriteType> SixthBeat { get; } = new(GROUP, "Sixth Beat", SpriteType.Star, "Sprite for the shadows of monsters aligned to the sixth-beat.");
        public static Setting<SpriteType> QuarterBeat { get; } = new(GROUP, "Quarter Beat", SpriteType.LeftTriangle, "Sprite for the shadows of monsters aligned to the quarter-beat.");
        public static Setting<SpriteType> ThirdBeat { get; } = new(GROUP, "Third Beat", SpriteType.LeftTrapezoid, "Sprite for the shadows of monsters aligned to the third-beat.");
        public static Setting<SpriteType> HalfBeat { get; } = new(GROUP, "Half Beat", SpriteType.Diamond, "Sprite for the shadows of monsters aligned to the half-beat.");
        public static Setting<SpriteType> TwoThirdBeat { get; } = new(GROUP, "Two-Third Beat", SpriteType.RightTrapezoid, "Sprite for the shadows of monsters aligned to the two-third-beat.");
        public static Setting<SpriteType> ThreeQuarterBeat { get; } = new(GROUP, "Three-Quarter Beat", SpriteType.RightTriangle, "Sprite for the shadows of monsters aligned to the three-quarter-beat.");
        public static Setting<SpriteType> FiveSixthBeat { get; } = new(GROUP, "Five-Sixth Beat", SpriteType.Star, "Sprite for the shadows of monsters aligned to the five-sixth-beat.");
        public static Setting<SpriteType> OtherBeat { get; } = new(GROUP, "Other", SpriteType.Star, "Sprite for the shadow of all other monsters.");
        
        public static SpriteType GetSpriteType(BeatType beatType) => beatType switch {
            BeatType.OnBeat => OnBeat,
            BeatType.SixthBeat => SixthBeat,
            BeatType.QuarterBeat => QuarterBeat,
            BeatType.ThirdBeat => ThirdBeat,
            BeatType.HalfBeat => HalfBeat,
            BeatType.TwoThirdBeat => TwoThirdBeat,
            BeatType.ThreeQuarterBeat => ThreeQuarterBeat,
            BeatType.FiveSixthBeat => FiveSixthBeat,
            _ => OtherBeat
        };
    }

    public static class Colors {
        public const string GROUP = "Custom Colors";
        public static readonly Color PURPLE = new(0.6f, 0.5f, 1f);
        public static readonly Color RED = new(1f, 0.4f, 0.5f);
        public static readonly Color YELLOW = new(1f, 0.9f, 0.6f);
        public static readonly Color GREEN = new(0.5f, 1f, 0.6f);
        public static readonly Color BLUE = new(0.3f, 0.7f, 1f);
        public static Setting<Color> OnBeat { get; } = new(GROUP, "On Beat", PURPLE, "Color for the shadows of monsters aligned to the beat.");
        public static Setting<Color> SixthBeat { get; } = new(GROUP, "Sixth Beat", RED, "Color for the shadows of monsters aligned to the sixth-beat.");
        public static Setting<Color> QuarterBeat { get; } = new(GROUP, "Quarter Beat", YELLOW, "Color for the shadows of monsters aligned to the quarter-beat.");
        public static Setting<Color> ThirdBeat { get; } = new(GROUP, "Third Beat", GREEN, "Color for the shadows of monsters aligned to the third-beat.");
        public static Setting<Color> HalfBeat { get; } = new(GROUP, "Half Beat", BLUE, "Color for the shadows of monsters aligned to the half-beat.");
        public static Setting<Color> TwoThirdBeat { get; } = new(GROUP, "Two-Third Beat", GREEN, "Color for the shadows of monsters aligned to the two-third-beat.");
        public static Setting<Color> ThreeQuarterBeat { get; } = new(GROUP, "Three-Quarter Beat", YELLOW, "Color for the shadows of monsters aligned to the three-quarter-beat.");
        public static Setting<Color> FiveSixthBeat { get; } = new(GROUP, "Five-Sixth Beat", RED, "Color for the shadows of monsters aligned to the five-sixth-beat.");
        public static Setting<Color> OtherBeat { get; }  = new(GROUP, "Other", RED, "Color for the shadow of all other monsters.");

        public static Color GetColor(BeatType beatType) => beatType switch {
            BeatType.OnBeat => OnBeat,
            BeatType.SixthBeat => SixthBeat,
            BeatType.QuarterBeat => QuarterBeat,
            BeatType.ThirdBeat => ThirdBeat,
            BeatType.HalfBeat => HalfBeat,
            BeatType.TwoThirdBeat => TwoThirdBeat,
            BeatType.ThreeQuarterBeat => ThreeQuarterBeat,
            BeatType.FiveSixthBeat => FiveSixthBeat,
            _ => OtherBeat
        };
    }
}
