namespace ShadowsReanimated;


public class CustomPreset : Preset {
    public override SpriteType GetSpriteType(BeatType beatType) => beatType switch {
        BeatType.OnBeat => Config.Custom.OnBeat.Value,
        BeatType.SixthBeat => Config.Custom.SixthBeat.Value,
        BeatType.QuarterBeat => Config.Custom.QuarterBeat.Value,
        BeatType.ThirdBeat => Config.Custom.ThirdBeat.Value,
        BeatType.HalfBeat => Config.Custom.HalfBeat.Value,
        BeatType.TwoThirdBeat => Config.Custom.TwoThirdBeat.Value,
        BeatType.ThreeQuarterBeat => Config.Custom.ThreeQuarterBeat.Value,
        BeatType.FiveSixthBeat => Config.Custom.FiveSixthBeat.Value,
        _ => Config.Custom.OtherBeat.Value
    };
}
