using System;
using System.Diagnostics;
using UnityExplorer;
using UnityModManagerNet;
using HarmonyLib;
using Kingmaker;

namespace UMMUnityExplorer
{
	public class Main
	{
		[Conditional("DEBUG")]
		internal static void Log(string msg)
		{
			Main.Logger.Log(msg);
		}

		internal static void Error(Exception ex)
		{
			UnityModManager.ModEntry.ModLogger logger = Main.Logger;
			if (logger != null)
			{
				logger.Error(ex.ToString());
			}
		}

		internal static void Error(string msg)
		{
			UnityModManager.ModEntry.ModLogger logger = Main.Logger;
			if (logger != null)
			{
				logger.Error(msg);
			}
		}

		internal static UnityModManager.ModEntry.ModLogger Logger { get; private set; }
		internal static Harmony harmony;
		internal static bool Load(UnityModManager.ModEntry modEntry)
		{
			
			Main.Logger = modEntry.Logger;
			modEntry.OnUpdate = OnUpdate;
			harmony = new Harmony(modEntry.Info.Id);
			harmony.PatchAll();
			return true;
		}

		public static void OnUpdate(UnityModManager.ModEntry modEntry, float f)
        {
            UnityEngine.Debug.developerConsoleVisible = false;
		}

		[HarmonyPatch]
		public static class ContentInjector
		{

			[HarmonyPatch(typeof(MainMenu), "Awake"), HarmonyPostfix]
			public static void MainMenu_Awake()
			{
				ExplorerStandalone.CreateInstance((msg, logType) =>
				{
					Main.Logger.Error($"{logType}: {msg}");
				});
			}
		}
	}
}
