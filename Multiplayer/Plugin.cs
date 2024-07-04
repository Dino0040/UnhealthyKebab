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
        __instance.maxPlayerDropdown.AddOptions(new System.Collections.Generic.List<string>(new string[] { "5", "6", "7", "8" }));
		//logger.LogMessage("Added 5-8 player options!");
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(DartGame), nameof(DartGame.ResetBoard))]
    static bool DisableDartGame(DartGame __instance)
    {
        __instance.gameObject.SetActive(false);
        return false;
    }
	
	[HarmonyPrefix]
	[HarmonyPatch(typeof(PhotonPlayerListManager), "OnEnable")]
	static void ExtendPlayerButtonsList(PhotonPlayerListManager __instance, ref LobbySlot[] ___playerSlots)
	{
		Transform slotListTransform = __instance.transform.Find("UserSlots");
		if (!slotListTransform)
		{
			logger.LogMessage("ERROR: No UserSlots found!");
			return;
		}
		GameObject slotListObject = slotListTransform.gameObject;

		RectTransform slotListRectTransform = slotListObject.GetComponent<RectTransform>();
		slotListRectTransform.anchorMin = new Vector2(0.3f, 0.0f);
		slotListRectTransform.anchorMax = new Vector2(0.7f, 0.24f);
		slotListRectTransform.offsetMin = new Vector2(-200.0f, 0.0f);
		slotListRectTransform.offsetMax = new Vector2(200.0f, 0.0f);

		HorizontalLayoutGroup slotListHorizontalLayoutGroup = slotListObject.GetComponent<HorizontalLayoutGroup>();
		slotListHorizontalLayoutGroup.spacing = 0;
		slotListHorizontalLayoutGroup.childForceExpandHeight = true;
		slotListHorizontalLayoutGroup.childForceExpandWidth = false;
		slotListHorizontalLayoutGroup.spacing = 0;
		if (PhotonNetwork.room.MaxPlayers < 4)
		{
			slotListHorizontalLayoutGroup.spacing = 100;
		}
		if (PhotonNetwork.room.MaxPlayers == 4)
		{
			slotListHorizontalLayoutGroup.spacing = 50;
		}

		while (slotListObject.transform.childCount < PhotonNetwork.room.MaxPlayers)
		{
			GameObject newSlotListElement = GameObject.Instantiate(slotListObject.transform.GetChild(0).gameObject);
			newSlotListElement.transform.parent = slotListObject.transform;
			newSlotListElement.transform.localScale = Vector3.one;
			newSlotListElement.GetComponent<LobbySlot>().ResetSlot();
		}

		//logger.LogMessage("Aligned and added invite buttons!");
	}
}
