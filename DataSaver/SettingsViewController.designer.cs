// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace DataSaver
{
	[Register ("SettingsViewController")]
	partial class SettingsViewController
	{
		[Outlet]
		AppKit.NSTableView ActionsTable { get; set; }

		[Outlet]
		AppKit.NSButton DeleteActionButton { get; set; }

		[Outlet]
		AppKit.NSButton DeleteWifiButton { get; set; }

		[Outlet]
		AppKit.NSButton RunOnStartup { get; set; }

		[Outlet]
		AppKit.NSTableView WifiTable { get; set; }

		[Action ("AddAction:")]
		partial void AddAction (Foundation.NSObject sender);

		[Action ("AddWifi:")]
		partial void AddWifi (Foundation.NSObject sender);

		[Action ("DeleteAction:")]
		partial void DeleteAction (Foundation.NSObject sender);

		[Action ("DeleteWifi:")]
		partial void DeleteWifi (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (ActionsTable != null) {
				ActionsTable.Dispose ();
				ActionsTable = null;
			}

			if (DeleteActionButton != null) {
				DeleteActionButton.Dispose ();
				DeleteActionButton = null;
			}

			if (DeleteWifiButton != null) {
				DeleteWifiButton.Dispose ();
				DeleteWifiButton = null;
			}

			if (RunOnStartup != null) {
				RunOnStartup.Dispose ();
				RunOnStartup = null;
			}

			if (WifiTable != null) {
				WifiTable.Dispose ();
				WifiTable = null;
			}
		}
	}
}
