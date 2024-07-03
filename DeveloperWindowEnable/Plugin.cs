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

    [HarmonyPrefix]
    [HarmonyPatch(typeof(PhotonMultiplayerHandler), nameof(PhotonMultiplayerHandler.OnClickCreateRoom))]
    static void SetMaxPlayersTo6(string roomName, string roomPassword, ref int maxPlayers, bool inviteOnly)
    {
        maxPlayers = 6;
        logger.LogMessage("Enabled 6 Players!");
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(photonButtons), "Start")]
    static void AddMoreOptionsToPlayerDropdown(photonButtons __instance)
    {
        __instance.maxPlayerDropdown.AddOptions(new System.Collections.Generic.List<string>(new string[] { "5", "6" }));
    }
}
