using System;
using System.Collections.Generic;

namespace DataSaver
{
	public static class StateManager
	{

		static StateManager ()
		{
			Helpers = new List<BaseHelper> {
				new DropboxHelper (),
			};
			
			var back = new BackBlazeHelper ();
			if (back.IsInstalled)
				Helpers.Add (back);
		}

		public static Action StateChanged { get; set; }

		static bool isPaused;

		public static bool IsPaused {
			get {
				return isPaused;
			}
			set {
				isPaused = value;
				StateChanged?.Invoke ();
			}
		}


		static bool isEnabled = true;

		public static bool IsEnabled {
			get {
				return isEnabled;
			}
			set {
				isEnabled = value;
				StateChanged?.Invoke ();
			}
		}

		public static void ToggleEnabled ()
		{
			if (IsEnabled)
				Resume ();
			IsEnabled = !IsEnabled;
		}

		public static void TogglePaused ()
		{
			if (!IsPaused)
				PauseEverything ();
			else
				Resume ();
		}

		public static List<BaseHelper> Helpers;
		public static void PauseEverything ()
		{
			if (IsPaused)
				return;
			IsPaused = true;
			Helpers.ForEach (x => x.Pause ());
		}

		public static void Resume ()
		{
			if (!IsPaused)
				return;
			IsPaused = false;
			Helpers.ForEach (x => x.Resume ());
			
		}
	}
}

