using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShadowsReanimated;

public enum ProfileType {
    Default,
    Delta,
    Kappa,
    Vanilla,
    Custom
}

public class Profile(params (BeatType, SpriteType)[] sprites) {
    private static readonly Dictionary<ProfileType, Profile> profiles = new() {
        [ProfileType.Default] = new(
            (BeatType.QuarterBeat, SpriteType.LeftTriangle),
            (BeatType.ThirdBeat, SpriteType.LeftTrapezoid),
            (BeatType.TwoThirdBeat, SpriteType.RightTrapezoid),
            (BeatType.ThreeQuarterBeat, SpriteType.RightTriangle)
        ),
        [ProfileType.Delta] = new(
            (BeatType.SixthBeat, SpriteType.LeftTrapezoid),
            (BeatType.QuarterBeat, SpriteType.HollowSquare),
            (BeatType.ThirdBeat, SpriteType.LeftTriangle),
            (BeatType.TwoThirdBeat, SpriteType.RightTriangle),
            (BeatType.ThreeQuarterBeat, SpriteType.HollowDiamond),
            (BeatType.FiveSixthBeat, SpriteType.RightTrapezoid)
        ),
        [ProfileType.Kappa] = new(
            (BeatType.QuarterBeat, SpriteType.HollowCircle),
            (BeatType.ThirdBeat, SpriteType.Triangle),
            (BeatType.TwoThirdBeat, SpriteType.HollowTriangle),
            (BeatType.ThreeQuarterBeat, SpriteType.HollowDiamond)
        ),
        [ProfileType.Custom] = new CustomProfile()
    };
    public static Profile Current => profiles.GetValueOrDefault(Config.General.Profile.Value);

    private readonly Dictionary<BeatType, SpriteType> sprites = sprites.ToDictionary(x => x.Item1, x => x.Item2);

    public virtual SpriteType GetSpriteType(BeatType beatType) => sprites.GetValueOrDefault(beatType, sprites.GetValueOrDefault(BeatType.OtherBeat, SpriteType.Star));
    
    public Sprite GetSprite(BeatType beatType) => Assets.GetSprite(GetSpriteType(beatType), beatType);
}
