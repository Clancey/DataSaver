using System;
using AppKit;
using Foundation;
using Xamarin.Tables;
namespace DataSaver
{
	public class WifiCell : ICell
	{
		public WifiCell()
		{
		}
		public WiFiClass Wifi { get; set; }
		public NSView GetCell(NSTableView tableView, NSTableColumn tableColumn, NSObject owner)
		{
			if (tableColumn.Identifier == "Enabled")
			{
				var check = tableView.MakeView("Checkbox", owner) as CheckBox ?? new CheckBox();
				check.Checked = Wifi.Enabled;
				check.ValueChanged = (value) =>
				{
					Wifi.Enabled = value;
					App.WiFiViewModel.Add(Wifi);
				};
				return check;
			}

			if (tableColumn.Identifier == "Edit")
			{
				var linkButton = tableView.MakeView("SimpleButton", owner) as SimpleButton  ?? new SimpleButton ();
				linkButton.Title = "Edit";
				linkButton.Tapped = (b) =>
				{
					var text = App.GetTextInput("SSID", "SSID that start with *. will block all that contain that word", Wifi.SSID);
					if (string.IsNullOrWhiteSpace(text) || text == Wifi.SSID)
						return;
					App.WiFiViewModel.Delete(Wifi);
					Wifi.SSID = text;
					App.WiFiViewModel.Add(Wifi);
				};
				return linkButton;
			}


			var textField = tableView.MakeView("Text", owner) as NSTextField ?? new NSTextField();
			textField.Editable = false;
			textField.Bordered = false;
			textField.StringValue = Wifi.SSID;
			return textField;
		}

		public string GetCellText(NSTableColumn tableColumn)
		{
			if (tableColumn.Identifier == "SSID")
				return Wifi.SSID;
			return null;
		}
	}
}

