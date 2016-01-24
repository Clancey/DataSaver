using System;
using System.Collections.Generic;
using System.Linq;

namespace DataSaver
{
	public static class StateManager
	{

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
		public static Dictionary<int,BaseHelper> Helpers;
		public static void PauseEverything ()
		{
			if (IsPaused)
				return;
			IsPaused = true;
			var helpers = App.ActionsViewModel.Actions.Select(x => BaseHelper.CreateHelper(x,true)).ToList();
			helpers.ForEach (x => x.Pause ());
		}

		public static void Resume ()
		{
			if (!IsPaused)
				return;
			IsPaused = false;

			var helpers = App.ActionsViewModel.Actions.Select(x => BaseHelper.CreateHelper(x,false)).ToList();
			helpers.ForEach (x => x.Resume ());
			
		}


	}
}

