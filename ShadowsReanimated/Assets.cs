using UnityEngine;

namespace ShadowsReanimated;

public static class Assets {
    public static Sprite QuarterBeat { get; private set; }
    public static Sprite ThirdBeat { get; private set; }
    public static Sprite TwoThirdBeat { get; private set; }
    public static Sprite ThreeQuarterBeat { get; private set; }
    
    private static Sprite MakeSprite(byte[] data) {
        Texture2D tex = new(0, 0);
        tex.LoadImage(data);
        return Sprite.Create(
            tex,
            new(0, 0, tex.width, tex.height),
            new(0.5f, 0.5f),
            48
        );
    }

    public static void Initialize() {
        QuarterBeat = MakeSprite(Properties.Resources.QuarterBeat);
        ThirdBeat = MakeSprite(Properties.Resources.ThirdBeat);
        TwoThirdBeat = MakeSprite(Properties.Resources.TwoThirdBeat);
        ThreeQuarterBeat = MakeSprite(Properties.Resources.ThreeQuarterBeat);
    }
}
