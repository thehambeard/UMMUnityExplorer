using System;
using System.Diagnostics;
using UnityExplorer;
using UnityModManagerNet;

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

		internal static bool Load(UnityModManager.ModEntry modEntry)
		{
			try
			{
				Main.Logger = modEntry.Logger;
				ExplorerStandalone.CreateInstance();
				modEntry.OnUpdate = OnUpdate;

			}
			catch (Exception ex)
			{
				Main.Error(ex);
				throw;
			}
			return true;
		}

		public static void OnUpdate(UnityModManager.ModEntry modEntry, float f)
        {
            UnityEngine.Debug.developerConsoleVisible = false;
		}
	}
}
