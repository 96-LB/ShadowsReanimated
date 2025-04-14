using System.Runtime.CompilerServices;
using UnityEngine;

namespace ShadowsReanimated;


internal static class DebugUtil {
    private static void PrintAllComponents(GameObject gameObject, int depth = 1) {
        foreach(var component in gameObject.GetComponents<Component>()) {
            Plugin.Log.LogWarning($"{new string(' ', depth * 2)}• {component.GetType().Name}");
        }
    }

    internal static void PrintAllComponents(GameObject gameObject) {
        Plugin.Log.LogWarning($"Components of [{gameObject}]:");
        PrintAllComponents(gameObject, 1);
    }
    internal static void PrintAllComponents(Transform transform) {
        PrintAllComponents(transform.gameObject);
    }

    internal static void PrintAllComponents(Component component) {
        PrintAllComponents(component.gameObject);
    }   
    
    private static void PrintAllChildren(Transform transform, int depth, bool recursive = false, bool components = false) {
        if(components) {
            PrintAllComponents(transform.gameObject, depth + 1);
        }
        foreach(Transform child in transform) {
            Plugin.Log.LogWarning($"{new string(' ', depth * 2)}○ {child.name}");
            if(recursive) {
                PrintAllChildren(child, depth + 1, recursive, components);
            }
        }
    }   
    
    internal static void PrintAllChildren(Transform transform, bool recursive = false, bool components = false) {
        Plugin.Log.LogWarning($"Children of [{transform}]:");
        PrintAllChildren(transform, 1, recursive, components);
    }

    internal static void PrintAllChildren(GameObject gameObject, bool recursive = false, bool components = false) {
        PrintAllChildren(gameObject.transform, recursive, components);
    }

    internal static void PrintAllChildren(Component component, bool recursive = false, bool components = false) {
        PrintAllChildren(component.gameObject, recursive, components);
    }
}
