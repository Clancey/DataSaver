using System;
using System.IO;

namespace DataSaver
{
	public class DropboxHelper : BaseHelper
	{
		public DropboxHelper ()
		{
			
			Title = "Dropbox";
		}

		public override async void Pause ()
		{
			if(IsSandboxed)
				await RunAutomatorScript("pauseDropbox.workflow");
			else
				await RunProcess("pkill","-stop  Dropbox");
		}

		public override async void Resume ()
		{

			if(IsSandboxed)
				await RunAutomatorScript("resumeDropbox.workflow");
			else
				await RunProcess("pkill","-cont  Dropbox");
		}

		public override bool IsInstalled {
			get {
				return IsDropboxInstalled;
			}
			set {
				base.IsInstalled = value;
			}
		}
		static string DropboxPath = Path.Combine(Locations.ApplicationsPath, "Dropbox.app");
		public static bool IsDropboxInstalled
		{
			get {
				if (App.IsSandboxed)
				{
					return AutomatorFileExists("pauseDropbox.workflow") && AutomatorFileExists("resumeDropbox.workflow");
				}
				return Directory.Exists(DropboxPath);
			}
		}
	}
}

