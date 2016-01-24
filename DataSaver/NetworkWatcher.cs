using System;
using CoreWlan;
using Foundation;
using System.Linq;
using ObjCRuntime;
using SystemConfiguration;
using System.Threading.Tasks;

namespace DataSaver
{
	public class NetworkWatcher
	{
		public NetworkWatcher ()
		{
		}

		static readonly NSString iosWifiKey = (NSString)"IOS_IE";

		public static string CurrentSSID
		{
			get
			{
				return CWInterface.MainInterface?.Ssid;
			}
		}
		public static bool IsIosTethered ()
		{
			try {
				var iface = CWInterface.MainInterface;
				NSError error;
				var networks = iface.ScanForNetworksWithSsid (null, out error);
				var network = networks.FirstOrDefault (x => x.Ssid == iface.Ssid);
				if (network == null)
					return false;
				var records = network.ScanRecords ();

				if (!records.ContainsKey (iosWifiKey))
					return false;
				return records [iosWifiKey] != null;
			} catch (Exception ex) {
				Console.WriteLine (ex);
				return false;
			}
		}

		public static bool IsiOSUsbTethered ()
		{
			try {
				return CWNetworkExtensions.IsIPhone ();
			} catch (Exception ex) {
				Console.WriteLine (ex);
				return false;
			}
		}

		public static async Task<bool> IsIphone ()
		{
			var wifi = Task.Run (() => IsIosTethered ());
			var usb = Task.Run (() => IsiOSUsbTethered ());

			var t = await Task.WhenAny (new[]{ wifi, usb });
			if (t.Result)
				return true;
			if (t == wifi)
				return usb.Result;
			return wifi.Result;
		}

		public static async Task<bool> ShouldPause()
		{
			var iPhoneTask = IsIphone();
			var isWifi = IsBlockedWifi();
			if (isWifi)
				return true;
			return await iPhoneTask;

		}

		public static bool IsBlockedWifi()
		{
			var ssid = CurrentSSID.ToLower();
			foreach (var wifi in App.WiFiViewModel.Wifis)
			{
				var checkContains = wifi.SSID.Contains("*.");
				var name = wifi.SSID.Replace("*.","");
				if (checkContains)
				{
					var contains = ssid.IndexOf(name, StringComparison.InvariantCultureIgnoreCase) >= 0;
					if (contains)
						return true;
					continue;
				}

				var isMatch = name.ToLower() == ssid;
				if (isMatch)
					return true;

			}
			return false;
		}
	}
}

