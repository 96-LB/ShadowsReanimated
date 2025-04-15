using UnityEngine;

namespace ShadowsReanimated;

public static class Assets {
    public static Sprite LeftTriangle { get; private set; }
    public static Sprite RightTriangle { get; private set; }
    public static Sprite LeftTrapezoid { get; private set; }
    public static Sprite RightTrapezoid { get; private set; }
    
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
        LeftTriangle = MakeSprite(Properties.Resources.LeftTriangle);
        RightTriangle = MakeSprite(Properties.Resources.RightTriangle);
        LeftTrapezoid = MakeSprite(Properties.Resources.LeftTrapezoid);
        RightTrapezoid = MakeSprite(Properties.Resources.RightTrapezoid);
    }
}
