namespace ShadowsReanimated;


public class CustomPreset : Preset {
    public override SpriteType GetSpriteType(BeatType beatType) => Config.Sprites.GetSpriteType(beatType);
}
