using UnityEngine;
using System.Collections.Generic;
using System;

namespace ShadowsReanimated;


public enum SpriteType {
    Circle,
    Diamond,
    Star,
    Triangle,
    Square,
    LeftTriangle,
    RightTriangle,
    LeftTrapezoid,
    RightTrapezoid,
    CircleRing,
    DiamondRing,
    TriangleRing,
    SquareRing,
    Custom
}


public static class Assets {
    private static readonly Dictionary<SpriteType, Sprite> sprites = [];
    private static readonly Dictionary<BeatType, Sprite> customs = [];

    public static Sprite GetSprite(SpriteType key, BeatType beat) => key switch {
        SpriteType.Custom => customs.GetValueOrDefault(beat),
        _ => sprites.GetValueOrDefault(key)
    };

    private static Sprite MakeSprite(byte[] data) {
        Texture2D tex = new(0, 0);
        tex.LoadImage(data);
        return Sprite.Create(
            tex,
            new(0, 0, tex.width, tex.height),
            new(0.5f, 0.5f),
            44
        );
    }

    public static void Initialize() {
        // load default sprites from resources
        var types = Enum.GetValues(typeof(SpriteType)) as SpriteType[];
        foreach(var type in types) {
            var data = Properties.Resources.ResourceManager.GetObject(Enum.GetName(typeof(SpriteType), type));
            if(data is byte[] img) {
                sprites[type] = MakeSprite(img);
            }
        }
    }
}
