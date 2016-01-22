using System;
using AppKit;
using CoreGraphics;
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
	}
}

