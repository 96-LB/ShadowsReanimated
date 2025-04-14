using System;
using HarmonyLib;
using RhythmRift.Enemies;
using UnityEngine;

namespace ShadowsReanimated.Patches;

using P = RREnemy;
using BeatState = State<RREnemy, BeatStateData>;

internal enum BeatType {
    OnBeat,
    QuarterBeat,
    ThirdBeat,
    HalfBeat,
    TwoThirdBeat,
    ThreeQuarterBeat,
    OtherBeat
}


internal class BeatStateData {
    internal BeatType beat;
}

internal static class ShadowPatch_Common {
    internal static BeatType GetBeatType(float beat, float TOL = 0.005f) {
        beat = (beat % 1 + 1) % 1; // get fractional component
        if(beat < TOL / 2 || beat > 1 - TOL / 2) {
            return BeatType.OnBeat;
        } else if(Mathf.Abs(beat - 1f / 4) < TOL) {
            return BeatType.QuarterBeat;
        } else if(Mathf.Abs(beat - 1f / 3) < TOL) {
            return BeatType.ThirdBeat;
        } else if(Mathf.Abs(beat - 1f / 2) < TOL) {
            return BeatType.HalfBeat;
        } else if(Mathf.Abs(beat - 2f / 3) < TOL) {
            return BeatType.TwoThirdBeat;
        } else if(Mathf.Abs(beat - 3f / 4) < TOL) {
            return BeatType.ThreeQuarterBeat;
        } else {
            return BeatType.OtherBeat;
        }
    }

    internal static void SetBeatSprite(
        SpriteRenderer renderer,
        BeatType beatType,
        Sprite onBeat,
        Sprite halfBeat,
        Sprite otherBeat
     ) {
        if((bool)renderer) {
            renderer.sprite = beatType switch {
                BeatType.OnBeat => onBeat,
                BeatType.QuarterBeat => Plugin.QuarterBeat,
                BeatType.ThirdBeat => Plugin.ThirdBeat,
                BeatType.HalfBeat => halfBeat,
                BeatType.TwoThirdBeat => Plugin.TwoThirdBeat,
                BeatType.ThreeQuarterBeat => Plugin.ThreeQuarterBeat,
                BeatType.OtherBeat => otherBeat,
                _ => throw new NotImplementedException()
            };
        }
    }

    internal static void FlipSprite(SpriteRenderer renderer, Transform scalePointTransform) {
        if(renderer) {
            renderer.flipX = scalePointTransform.localScale.x < 0;
        }
    }
}

[HarmonyPatch(typeof(P), nameof(P.Initialize))]
internal static class ShadowPatch_Initialize {
    private static void Postfix(
        P __instance,
        SpriteRenderer ____monsterShadow,
        Sprite ____onBeatShadowSprite,
        Sprite ____halfBeatShadowSprite,
        Sprite ____otherBeatShadowSprite,
        Transform ____scalePointTransform
    ) {
        if(!Config.MoreShadows.Enabled) {
            return;
        }

        var beatType = ShadowPatch_Common.GetBeatType(__instance.SpawnTrueBeatNumber);
        BeatState.Of(__instance).beat = beatType;
        ShadowPatch_Common.SetBeatSprite(
            ____monsterShadow,
            beatType,
            ____onBeatShadowSprite,
            ____halfBeatShadowSprite,
            ____otherBeatShadowSprite
        );

        ShadowPatch_Common.FlipSprite(____monsterShadow, ____scalePointTransform);
    }
}


[HarmonyPatch(typeof(P), nameof(P.UpdateState))]
internal static class ShadowPatch_UpdateState {
    private static void Prefix(
        P __instance,
        ref bool ____isOnBeat,
        ref bool ____isHalfBeat,
        SpriteRenderer ____monsterShadow,
        Sprite ____onBeatShadowSprite,
        Sprite ____halfBeatShadowSprite,
        Sprite ____otherBeatShadowSprite,
        Transform ____scalePointTransform
    ) {
        if(!Config.MoreShadows.Enabled) {
            return;
        }

        // prevent the original code from overwriting our shadow
        ____isOnBeat = __instance.NextActionRowTrueBeatNumber % 1f <= 0.05f || __instance.NextActionRowTrueBeatNumber % 1f >= 0.95f;
        ____isHalfBeat = __instance.NextActionRowTrueBeatNumber % 1f >= 0.45f && __instance.NextActionRowTrueBeatNumber % 1f <= 0.55f;

        // set the shadow ourselves
        var beatType = ShadowPatch_Common.GetBeatType(__instance.NextActionRowTrueBeatNumber);
        var state = BeatState.Of(__instance);
        if(beatType != state.beat && float.IsFinite(__instance.NextActionRowTrueBeatNumber)) {
            state.beat = beatType;
            ShadowPatch_Common.SetBeatSprite(
                ____monsterShadow,
                beatType,
                ____onBeatShadowSprite,
                ____halfBeatShadowSprite,
                ____otherBeatShadowSprite
            );
        }

        ShadowPatch_Common.FlipSprite(____monsterShadow, ____scalePointTransform);
    }
}

[HarmonyPatch(typeof(P), nameof(P.UpdateAnimations))]
internal static class ShadowPatch_UpdateAnimations {
    private static void Postfix(
        SpriteRenderer ____monsterShadow,
        Transform ____scalePointTransform
    ) {
        if(!Config.MoreShadows.Enabled) {
            return;
        }

        ShadowPatch_Common.FlipSprite(____monsterShadow, ____scalePointTransform);
    }
}
