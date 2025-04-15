using HarmonyLib;
using RhythmRift.Enemies;
using UnityEngine;

namespace ShadowsReanimated.Patches;


public class ShadowState : State<RREnemy, ShadowState> {
    public SpriteRenderer Shadow => Instance._monsterShadow;
    public BeatType Beat { get; private set; }
    public Profile Profile { get; private set; }

    public void SetShadow(Profile profile, BeatType beat, bool force = false) {
        if(!force && profile == Profile && beat == Beat && profile is not CustomProfile) {
            return;
        }

        Profile = profile;
        Beat = beat;
        if(Shadow) {
            Shadow.sprite = Profile?.GetSprite(Beat) ?? Beat switch {
                BeatType.OnBeat => Instance._onBeatShadowSprite,
                BeatType.HalfBeat => Instance._halfBeatShadowSprite,
                _ => Instance._otherBeatShadowSprite
            };
            if(Shadow.sprite == Instance._otherBeatShadowSprite) {
                Plugin.Log.LogWarning($"{Instance.DisplayName} {Instance.SpawnTrueBeatNumber} {Instance.NextActionRowTrueBeatNumber}");
            }
        }
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
        state.SetShadow(Profile.Current, beat, force: true);
        state.FlipSpriteIfNeeded();
    }

    [HarmonyPrefix]
    [HarmonyPatch(nameof(RREnemy.UpdateState))]
    public static void UpdateState(RREnemy __instance) {
        // set the shadow ourselves
        if(float.IsFinite(__instance.NextActionRowTrueBeatNumber)) {
            var state = ShadowState.Of(__instance);
            var beat = Beat.GetBeatType(__instance.NextActionRowTrueBeatNumber);
            state.SetShadow(Profile.Current, beat);
        }

        // prevent the original code from overwriting our shadow
        __instance._isOnBeat = __instance.NextActionRowTrueBeatNumber % 1f <= 0.05f || __instance.NextActionRowTrueBeatNumber % 1f >= 0.95f;
        __instance._isHalfBeat = __instance.NextActionRowTrueBeatNumber % 1f >= 0.45f && __instance.NextActionRowTrueBeatNumber % 1f <= 0.55f;
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(RREnemy.UpdateAnimations))]
    public static void UpdateAnimations(RREnemy __instance) {
        var state = ShadowState.Of(__instance);
        state.FlipSpriteIfNeeded();
    }
}
