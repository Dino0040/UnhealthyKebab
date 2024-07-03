using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine.Experimental.AI;

[BepInPlugin("Dino0040.KebabChefs.MorePlayers", "More Players", CommonPluginConstants.Version)]
public class Plugin : BaseUnityPlugin
{
    public static ManualLogSource logger;

    private void Awake()
    {
        logger = Logger;
        Harmony.CreateAndPatchAll(typeof(Plugin));
        logger.LogMessage("More Players mod loaded!");
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(photonButtons), "Start")]
    static void AddMoreOptionsToPlayerDropdown(photonButtons __instance)
    {
        __instance.maxPlayerDropdown.AddOptions(new System.Collections.Generic.List<string>(new string[] { "5", "6" }));
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(DartGame), nameof(DartGame.ResetBoard))]
    static bool DisableDartGame(DartGame __instance)
    {
        __instance.gameObject.SetActive(false);
        return false;
    }
}
