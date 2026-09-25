using UnityEngine;

namespace ShadowsReanimated;


public enum BeatType {
    OnBeat,
    SixthBeat,
    QuarterBeat,
    ThirdBeat,
    HalfBeat,
    TwoThirdBeat,
    ThreeQuarterBeat,
    FiveSixthBeat,
    OtherBeat
}

public static class Beat {
    internal static BeatType GetBeatType(float beat) {
        float tolerance = Mathf.Pow(10, Config.General.Tolerance);
        
        beat = (beat % 1 + 1) % 1; // get fractional component
        bool approx(float x) => Mathf.Abs(beat - x) < tolerance; // check if beat is approximately equal to x
        
        return beat switch {
            _ when beat < tolerance / 2 || beat > 1 - tolerance / 2 => BeatType.OnBeat,
            _ when approx(1f / 6) => BeatType.SixthBeat,
            _ when approx(1f / 4) => BeatType.QuarterBeat,
            _ when approx(1f / 3) => BeatType.ThirdBeat,
            _ when approx(1f / 2) => BeatType.HalfBeat,
            _ when approx(2f / 3) => BeatType.TwoThirdBeat,
            _ when approx(3f / 4) => BeatType.ThreeQuarterBeat,
            _ when approx(5f / 6) => BeatType.FiveSixthBeat,
            _ => BeatType.OtherBeat
        };
    }
}
