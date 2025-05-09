using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShadowsReanimated;

public enum PresetType {
    Default,
    Delta,
    Katie,
    Vanilla,
    Custom
}

public class Preset(params (BeatType, SpriteType)[] sprites) {
    private static readonly Dictionary<PresetType, Preset> presets = new() {
        [PresetType.Default] = new(
            (BeatType.QuarterBeat, SpriteType.LeftTriangle),
            (BeatType.ThirdBeat, SpriteType.LeftTrapezoid),
            (BeatType.TwoThirdBeat, SpriteType.RightTrapezoid),
            (BeatType.ThreeQuarterBeat, SpriteType.RightTriangle)
        ),
        [PresetType.Delta] = new(
            (BeatType.SixthBeat, SpriteType.LeftTrapezoid),
            (BeatType.QuarterBeat, SpriteType.HollowSquare),
            (BeatType.ThirdBeat, SpriteType.LeftTriangle),
            (BeatType.TwoThirdBeat, SpriteType.RightTriangle),
            (BeatType.ThreeQuarterBeat, SpriteType.HollowDiamond),
            (BeatType.FiveSixthBeat, SpriteType.RightTrapezoid)
        ),
        [PresetType.Katie] = new(
            (BeatType.QuarterBeat, SpriteType.HollowCircle),
            (BeatType.ThirdBeat, SpriteType.Triangle),
            (BeatType.TwoThirdBeat, SpriteType.HollowTriangle),
            (BeatType.ThreeQuarterBeat, SpriteType.HollowDiamond)
        ),
        [PresetType.Custom] = new CustomPreset()
    };
    public static Preset Current => presets.GetValueOrDefault(Config.General.Preset.Value);

    private readonly Dictionary<BeatType, SpriteType> sprites = sprites.ToDictionary(x => x.Item1, x => x.Item2);

    public virtual SpriteType GetSpriteType(BeatType beatType) => sprites.GetValueOrDefault(beatType, sprites.GetValueOrDefault(BeatType.OtherBeat, SpriteType.Star));
    
    public Sprite GetSprite(BeatType beatType) => Assets.GetSprite(GetSpriteType(beatType), beatType);
}
