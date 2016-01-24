using System;
using System.IO;
using System.Linq;
using AppKit;
using CoreGraphics;
using Foundation;
namespace DataSaver
{
	public static class App
	{
		public static string GetTextInput(string message, string informative,string placeholder = "")
		{
			var msg = new NSAlert();
			msg.AddButton("OK");
			msg.AddButton("Cancel");
			msg.MessageText = message;
			msg.InformativeText = informative;

			var txt = new NSTextField(new CGRect(0, 0, 200, 24));
			txt.StringValue = placeholder;

			msg.AccessoryView = txt;
			var resp = msg.RunSheetModal(AppDelegate.CurrentWindow);
			if (resp == (int)NSAlertButtonReturn.First)
				return txt.StringValue;
			return null;
		}

		static bool? _isSandboxed;
		public static bool IsSandboxed
		{
			get{
				if (!_isSandboxed.HasValue) {
					try{
						var path = System.Environment.GetFolderPath (Environment.SpecialFolder.MyMusic);
						if(path.Contains("com.iis.DataSaver"))
							_isSandboxed = true;
						else{
							var files = Directory.EnumerateFiles (path).Count();
							_isSandboxed = false;
						}
					}
					catch(Exception ex) {
						_isSandboxed = true;
					}
				}
				return _isSandboxed.Value;
			}
		}

		public static NSUrl GetScriptPath
		{
			get
			{
				return NSFileManager.DefaultManager.GetUrls(NSSearchPathDirectory.ApplicationScriptsDirectory, NSSearchPathDomain.All).FirstOrDefault();
			}
		}

		public static readonly WifiViewModel WiFiViewModel = new WifiViewModel();


		public static readonly ActionsViewModel ActionsViewModel = new ActionsViewModel();

		public static async void CheckStatus()
		{

			if (!StateManager.IsEnabled)
				return;
			var shouldPause = await NetworkWatcher.ShouldPause ();
			if (shouldPause)
				StateManager.PauseEverything ();
			else
				StateManager.Resume ();
		}
	}
}

