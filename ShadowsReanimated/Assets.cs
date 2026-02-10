using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using RiftOfTheNecroManager;

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
        if(!tex.LoadImage(data)) {
            return null;
        }
        return Sprite.Create(
            tex,
            new(0, 0, tex.width, tex.height),
            new(0.5f, 0.5f),
            44
        );
    }

    static Assets() {
        // load default sprites from resources
        var spriteTypes = Enum.GetValues(typeof(SpriteType)) as SpriteType[];
        foreach(var type in spriteTypes) {
            var data = Properties.Resources.ResourceManager.GetObject(Enum.GetName(typeof(SpriteType), type));
            if(data is byte[] img) {
                sprites[type] = MakeSprite(img);
            }
        }

        // load custom sprites from the filesystem
        var path = PluginData.DataPath;
        if(!Directory.Exists(path)) {
            Log.Info($"No custom shadows directory found. The folder should be named '{PluginData.Name}' and located in the same directory as the game executable. No custom shadows will be loaded.");
            return;
        }

        var beatTypes = Enum.GetValues(typeof(BeatType)) as BeatType[];
        foreach(var type in beatTypes) {
            var file = Path.Combine(path, $"{Enum.GetName(typeof(BeatType), type)}.png");
            if(File.Exists(file)) {
                var data = File.ReadAllBytes(file);
                var sprite = MakeSprite(data);
                if(sprite) {
                    customs[type] = sprite;
                    Log.Info($"Loaded custom shadow sprite for {type}.");
                } else {
                    Log.Warning($"Failed to load custom shadow sprite for {type}. The file may not be a valid PNG image.");
                }
            } else {
                Log.Info($"No custom shadow sprite found for {type}. The file should be named '{Enum.GetName(typeof(BeatType), type)}.png' and located in the '{PluginData.Name}' directory.");
            }
        }
    }
}
