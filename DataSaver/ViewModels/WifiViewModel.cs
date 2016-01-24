using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Tables;
using Xamarin;
namespace DataSaver
{
	public class WifiViewModel : TableViewModel<WiFiClass>
	{

		public static WiFiClass[] DefaultWifis = new[]
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

		public List<WiFiClass> Wifis { get; set; } = new List<WiFiClass>();

		public WifiViewModel()
		{
			Wifis = Database.Main.Table<WiFiClass>().ToList();
			foreach (var wifi in DefaultWifis)
			{
				var exists = Wifis.Any(x => x.SSID.ToLower() == wifi.SSID.ToLower());
				if (exists)
					continue;
				Database.Main.InsertOrReplace(wifi);
				Wifis.Add(wifi);
			}
		}

		public void Add(WiFiClass wifi)
		{
			Database.Main.InsertOrReplace(wifi);
			Wifis = Database.Main.Table<WiFiClass>().ToList();

			App.CheckStatus();
			ReloadData();
		}

		public void Save(WiFiClass wifi)
		{
			Database.Main.InsertOrReplace(wifi);
			Wifis = Database.Main.Table<WiFiClass>().ToList();
			ReloadData();
		}

		public void Delete(int index)
		{
			try
			{
				Delete(Wifis[index]);
			}
			catch (Exception ex)
			{
				Insights.Report(ex);
			}
		}
		public void Delete(WiFiClass wifi)
		{
			Database.Main.Delete(wifi);
			Wifis = Database.Main.Table<WiFiClass>().ToList();
			App.CheckStatus();
			ReloadData();
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

