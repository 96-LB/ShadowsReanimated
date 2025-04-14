using System;
using HarmonyLib;
using RhythmRift.Enemies;
using UnityEngine;

namespace ShadowsReanimated.Patches;

using P = RREnemy;

public class ShadowState : State<P, ShadowState> {
    public SpriteRenderer Shadow => Instance._monsterShadow;

    private BeatType? beat;
    public BeatType Beat {
        get => beat ?? BeatType.OtherBeat;
        set {
            if(beat != value) {
                if(Shadow) {
                    Shadow.sprite = value switch {
                        BeatType.OnBeat => Instance._onBeatShadowSprite,
                        BeatType.QuarterBeat => Assets.QuarterBeat,
                        BeatType.ThirdBeat => Assets.ThirdBeat,
                        BeatType.HalfBeat => Instance._halfBeatShadowSprite,
                        BeatType.TwoThirdBeat => Assets.TwoThirdBeat,
                        BeatType.ThreeQuarterBeat => Assets.ThreeQuarterBeat,
                        BeatType.OtherBeat => Instance._otherBeatShadowSprite,
                        _ => throw new ArgumentException($"Invalid beat type: {value}")
                    };
                }
                beat = value;
            }
        }
    }

    public void FlipSpriteIfNeeded() {
        if(Shadow) {
            Shadow.flipX = Instance._scalePointTransform.localScale.x < 0;
        }
    }
}


[HarmonyPatch(typeof(P))]
public static class ShadowPatch {
    [HarmonyPostfix]
    [HarmonyPatch(nameof(P.Initialize))]
    public static void Initialize(P __instance) {
        if(!Config.General.Enabled) {
            return;
        }

        var beatType = Beat.GetBeatType(__instance.SpawnTrueBeatNumber);
        var state = ShadowState.Of(__instance);
        state.Beat = beatType;
        state.FlipSpriteIfNeeded();
    }

    [HarmonyPrefix]
    [HarmonyPatch(nameof(P.UpdateState))]
    public static void UpdateState(P __instance) {
        if(!Config.General.Enabled) {
            return;
        }

        // prevent the original code from overwriting our shadow
        __instance._isOnBeat = __instance.NextActionRowTrueBeatNumber % 1f <= 0.05f || __instance.NextActionRowTrueBeatNumber % 1f >= 0.95f;
        __instance._isHalfBeat = __instance.NextActionRowTrueBeatNumber % 1f >= 0.45f && __instance.NextActionRowTrueBeatNumber % 1f <= 0.55f;


        // set the shadow ourselves
        var state = ShadowState.Of(__instance);
        if(float.IsFinite(__instance.NextActionRowTrueBeatNumber)) {
            var beatType = Beat.GetBeatType(__instance.NextActionRowTrueBeatNumber);
            state.Beat = beatType;
        }

        state.FlipSpriteIfNeeded();
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(P.UpdateAnimations))]
    public static void UpdateAnimations(P __instance) {
        if(!Config.General.Enabled) {
            return;
        }

        var state = ShadowState.Of(__instance);
        state.FlipSpriteIfNeeded();
    }
}
