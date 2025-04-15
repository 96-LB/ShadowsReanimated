using UnityEngine;

namespace ShadowsReanimated;

public enum ProfileType {
    Default,
    Delta,
    Kappa,
    Vanilla,
    Custom
}

public class Profile {
    public static Profile Default { get; private set; }
    public static Profile Delta { get; private set; } // TODO: not implemented
    public static Profile Kappa { get; private set; } // TODO: not implemented
    public static Profile Vanilla { get; private set; }
    public static CustomProfile Custom { get; private set; }
    public static Profile Current => Config.General.Profile.Value switch {
        ProfileType.Default => Default,
        ProfileType.Delta => Delta,
        ProfileType.Kappa => Kappa,
        ProfileType.Vanilla => Vanilla,
        ProfileType.Custom => Custom,
        _ => Custom
    };

    public Sprite OnBeat { get; private set; }
    public Sprite SixthBeat { get; private set; }
    public Sprite QuarterBeat { get; private set; }
    public Sprite ThirdBeat { get; private set; }
    public Sprite HalfBeat { get; private set; }
    public Sprite TwoThirdBeat { get; private set; }
    public Sprite ThreeQuarterBeat { get; private set; }
    public Sprite FiveSixthBeat { get; private set; }
    public Sprite OtherBeat { get; private set; }

    public static void Initialize() {
        Default = new() {
            QuarterBeat = Assets.LeftTriangle,
            ThirdBeat = Assets.LeftTrapezoid,
            TwoThirdBeat = Assets.RightTrapezoid,
            ThreeQuarterBeat = Assets.RightTriangle,
        };
        Vanilla = new();
        Custom = new();
    }

    public Sprite GetSprite(BeatType beatType) {
        return beatType switch {
            BeatType.OnBeat => OnBeat,
            BeatType.SixthBeat => SixthBeat,
            BeatType.QuarterBeat => QuarterBeat,
            BeatType.ThirdBeat => ThirdBeat,
            BeatType.HalfBeat => HalfBeat,
            BeatType.TwoThirdBeat => TwoThirdBeat,
            BeatType.ThreeQuarterBeat => ThreeQuarterBeat,
            BeatType.FiveSixthBeat => FiveSixthBeat,
            _ => OtherBeat
        } ?? OtherBeat;
    }
}
