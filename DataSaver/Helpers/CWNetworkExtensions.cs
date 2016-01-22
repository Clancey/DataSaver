using System;
using CoreWlan;
using Foundation;
using ObjCRuntime;
using System.Runtime.InteropServices;
using CoreFoundation;
using System.Text;

namespace DataSaver
{
	public static class CWNetworkExtensions
	{
		static IntPtr _scanRecordsSel;

		static IntPtr scanRecordsSel {
			get {
				return _scanRecordsSel == IntPtr.Zero ? (_scanRecordsSel = Selector.GetHandle (GetString(recSelString))) : _scanRecordsSel;
					}
			}

		const string recSelString = "c2NhblJlY29yZA==";
		public static NSDictionary ScanRecords (this CWNetwork network)
		{
			var ret = Runtime.GetNSObject<NSDictionary> (intptr_objc_msgSend (network.Handle, scanRecordsSel));
			return ret;
		}

		static string GetString(string inString)
		{
			return Encoding.UTF8.GetString(Convert.FromBase64String(inString));
		}
		static string SetSTring(string instring)
		{

			return Convert.ToBase64String(Encoding.UTF8.GetBytes(instring));
		}
		const string LIBOBJC_DYLIB = "/usr/lib/libobjc.dylib";

		[DllImport (LIBOBJC_DYLIB, EntryPoint = "objc_msgSend")]
		public extern static IntPtr intptr_objc_msgSend (IntPtr receiver, IntPtr selector);



		[DllImport (Constants.SystemConfigurationLibrary)]
		extern static IntPtr /*CFArrayRef*/ SCNetworkInterfaceCopyAll ();


		[DllImport (Constants.SystemConfigurationLibrary)]
		extern static IntPtr /*CFArrayRef*/ SCNetworkInterfaceGetInterfaceType (IntPtr sface);

		[DllImport (Constants.SystemConfigurationLibrary)]
		extern static IntPtr /*CFArrayRef*/ _SCNetworkInterfaceGetIOPath (IntPtr sface);

		public static bool IsIPhone ()
		{
			IntPtr array = SCNetworkInterfaceCopyAll ();
			if (array == IntPtr.Zero) {
				//supportedInterfaces = null;
				return false;
			}

			var arrays = NSArray.ArrayFromHandle<NSObject> (array);
			foreach (var a in arrays) {
				if (a.Handle == IntPtr.Zero)
					continue;
				var type = FromCFStringIntptr (SCNetworkInterfaceGetInterfaceType (a.Handle));

				var address = FromCFStringIntptr (_SCNetworkInterfaceGetIOPath (a.Handle));
				if (type == "Ethernet" && address.IndexOf ("AppleUSBEthernetHost", StringComparison.CurrentCultureIgnoreCase) >= 0 && address.IndexOf ("iPhone", StringComparison.CurrentCultureIgnoreCase) >= 0)
					return true;
			}
			return false;
		}

		static string FromCFStringIntptr (IntPtr handle)
		{
			try {
				if (handle == IntPtr.Zero)
					return "";
				var cgString = new CFString (handle) ;
				return ((string)cgString) ?? "";
			} catch (Exception ex) {
				return "";
			}
		}
	}
}

