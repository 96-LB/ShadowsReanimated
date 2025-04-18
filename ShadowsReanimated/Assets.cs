using UnityEngine;
using System.Collections.Generic;

namespace ShadowsReanimated;

public static class Assets
{
    private static readonly Dictionary<string, Sprite> _sprites = new();

    public static Sprite GetSprite(string name)
    {
        if (_sprites.TryGetValue(name, out var sprite))
            return sprite;
        throw new KeyNotFoundException($"Sprite '{name}' not found.");
    }

    private static Sprite MakeSprite(byte[] data)
    {
        Texture2D tex = new(0, 0);
        tex.LoadImage(data);
        return Sprite.Create(
            tex,
            new Rect(0, 0, tex.width, tex.height),
            new Vector2(0.5f, 0.5f),
            48
        );
    }

    public static void Initialize()
    {
        var resourceMap = new Dictionary<string, byte[]>
        {
            { "LeftTriangle", Properties.Resources.LeftTriangle },
            { "RightTriangle", Properties.Resources.RightTriangle },
            { "LeftTrapezoid", Properties.Resources.LeftTrapezoid },
            { "RightTrapezoid", Properties.Resources.RightTrapezoid },
            { "CircleMinus", Properties.Resources.CircleMinus },
            { "CirclePlus", Properties.Resources.CirclePlus },
            { "HollowDiamond", Properties.Resources.HollowDiamond },
            { "HollowSquare", Properties.Resources.HollowSquare },
            { "LeftShortTrapezoid", Properties.Resources.LeftShortTrapezoid },
            { "RightShortTrapezoid", Properties.Resources.RightShortTrapezoid }
        };

        foreach (var kvp in resourceMap)
        {
            _sprites[kvp.Key] = MakeSprite(kvp.Value);
        }
    }
}
