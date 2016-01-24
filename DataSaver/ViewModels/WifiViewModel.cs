using System;
using System.Collections.Generic;
using Xamarin.Tables;
namespace DataSaver
{
	public class WifiViewModel : TableViewModel<WiFiClass>
	{
		public List<WiFiClass> Wifis = new List<WiFiClass>
		{
			new WiFiClass{
				Enabled = true,
				SSID = "*.iPhone",
			},
			new WiFiClass{
				Enabled = true,
				SSID="*.Android",
			},
			new WiFiClass{
				Enabled = true,
				SSID="gogoinflight",
			},
		};
		public WifiViewModel()
		{
		}

		public override string HeaderForSection(int section)
		{
			return "";
		}

		public override WiFiClass ItemFor(int section, int row)
		{
			return Wifis[row];
		}

		public override int NumberOfSections()
		{
			return 1;
		}

		public override int RowsInSection(int section)
		{
			return Wifis.Count;
		}

		public override ICell GetICell(int section, int row)
		{
			var item = ItemFor(section, row);
			return new WifiCell
			{
				Wifi = item,
			};
		}

		public Action SelectionsChanged { get; set; }
		public override void SelectionDidChange(Foundation.NSNotification notification)
		{
			SelectionsChanged?.Invoke();
			base.SelectionDidChange(notification);
		}
	}
}

