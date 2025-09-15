﻿using HarmonyLib;
using RhythmRift.Enemies;
using UnityEngine;

namespace ShadowsReanimated.Patches;


public class ShadowState : State<RREnemy, ShadowState> {
    public static Material VanillaMaterial { get; private set; }
    private static Material moddedMaterial;
    public static Material ModdedMaterial {
        get {
            if(!moddedMaterial) {
                moddedMaterial = new(Shader.Find("Sprites/Default"));
                moddedMaterial.SetColor(RREnemy.ColorShaderPropertyId, Color.white);
            }
            return moddedMaterial;
        }
    }
    public SpriteRenderer Shadow => Instance._monsterShadow;
    public BeatType Beat { get; private set; }
    public Preset Preset { get; private set; }

    public void SetShadow(Preset preset, BeatType beat, bool force = false) {
        if(!force && preset == Preset && beat == Beat && preset is not CustomPreset) {
            return;
        }

        Preset = preset;
        Beat = beat;
        if(Shadow) {
            Shadow.sprite = Preset?.GetSprite(Beat) ?? Beat switch {
                BeatType.OnBeat => Instance._onBeatShadowSprite,
                BeatType.HalfBeat => Instance._halfBeatShadowSprite,
                _ => Instance._otherBeatShadowSprite
            };
        }

        RefreshColor();
    }

    public void RefreshColor(bool forceMaterialRefresh = false) {
        if(!Shadow) {
            return;
        }

        if(!VanillaMaterial) {
            VanillaMaterial = Shadow.material;
        }

        // fall back to vanilla colors if the corresponding settings aren't active
        if(
            !Config.General.Colors.Value
            || (Instance._isPartOfVibeChain && !Config.General.VibeChainOverride.Value)
            || (Instance._isVibePowerActive && !Config.General.VibePowerOverride.Value)
        ) {
            // if the vanilla game called this function, we also need to reset the material
            if(forceMaterialRefresh) {
                Shadow.material = VanillaMaterial;
            }
            return;
        }
        
        Shadow.material = ModdedMaterial;
        Shadow.color = Config.Colors.GetColor(Beat);

        // the old shader properties mess with our color
        Shadow.GetPropertyBlock(Instance._enemyShadowMatPropBlock);
        Instance._enemyShadowMatPropBlock.SetColor(RREnemy.ColorShaderPropertyId, Color.white);
        Shadow.SetPropertyBlock(Instance._enemyShadowMatPropBlock);

        Shadow.name = "ShadowsReanimated"; // some monsters have an animation which force-sets the alpha channel to 1! this breaks the link to the animation
    }

    public void FlipSpriteIfNeeded() {
        if(Shadow) {
            Shadow.flipX = Instance._scalePointTransform.localScale.x < 0;
        }
    }
}


[HarmonyPatch(typeof(RREnemy))]
public static class ShadowPatch {
    [HarmonyPostfix]
    [HarmonyPatch(nameof(RREnemy.Initialize))]
    public static void Initialize(RREnemy __instance) {
        var state = ShadowState.Of(__instance);
        var beat = Beat.GetBeatType(__instance.SpawnTrueBeatNumber);
        state.SetShadow(Preset.Current, beat, force: true);
        state.FlipSpriteIfNeeded();
    }

    [HarmonyPrefix]
    [HarmonyPatch(nameof(RREnemy.UpdateState))]
    public static void UpdateState(RREnemy __instance) {
        // set the shadow ourselves
        if(float.IsFinite(__instance.NextActionRowTrueBeatNumber)) {
            var state = ShadowState.Of(__instance);
            var beat = Beat.GetBeatType(__instance.NextActionRowTrueBeatNumber);
            state.SetShadow(Preset.Current, beat);
            state.RefreshColor();
        }

        // prevent the original code from overwriting our shadow
        __instance._isOnBeat = __instance.NextActionRowTrueBeatNumber % 1f <= 0.05f || __instance.NextActionRowTrueBeatNumber % 1f >= 0.95f;
        __instance._isHalfBeat = __instance.NextActionRowTrueBeatNumber % 1f >= 0.45f && __instance.NextActionRowTrueBeatNumber % 1f <= 0.55f;
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(RREnemy.RefreshShadowColor))]
    public static void RefreshShadowColor(RREnemy __instance) {
        var state = ShadowState.Of(__instance);
        state.RefreshColor(forceMaterialRefresh: true);
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(RREnemy.UpdateAnimations))]
    public static void UpdateAnimations(RREnemy __instance) {
        var state = ShadowState.Of(__instance);
        state.FlipSpriteIfNeeded();
    }
}
