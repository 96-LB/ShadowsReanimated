using UnityEngine;
using System.Collections.Generic;

namespace ShadowsReanimated;


public enum SpriteType {
    Circle,
    Diamond,
    Star,
    Triangle, // TODO: missing sprite
    Square, // TODO: missing sprite
    LeftTriangle,
    RightTriangle,
    LeftTrapezoid,
    RightTrapezoid,
    HollowCircle, // TODO: missing sprite
    HollowDiamond, // TODO: needs edit
    HollowTriangle, // TODO: missing sprite
    HollowSquare, // TODO: needs edit
    Custom
}


public static class Assets {
    private static readonly Dictionary<SpriteType, byte[]> images = new() {
        [SpriteType.LeftTriangle] = Properties.Resources.LeftTriangle,
        [SpriteType.RightTriangle] = Properties.Resources.RightTriangle,
        [SpriteType.LeftTrapezoid] = Properties.Resources.LeftTrapezoid,
        [SpriteType.RightTrapezoid] = Properties.Resources.RightTrapezoid,
        [SpriteType.HollowDiamond] = Properties.Resources.HollowDiamond,
        [SpriteType.HollowSquare] = Properties.Resources.HollowSquare,
    };
    private static readonly Dictionary<SpriteType, Sprite> sprites = [];

    public static Sprite GetSprite(SpriteType key) => sprites.GetValueOrDefault(key);

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
        foreach(var (key, value) in images) {
            sprites[key] = MakeSprite(value);
        }
    }
}
