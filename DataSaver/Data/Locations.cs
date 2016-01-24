using System;
using System.IO;

namespace DataSaver
{
	public static class Locations
	{
		static Locations()
		{
			Directory.CreateDirectory(LibDir);
		}
		[System.Runtime.InteropServices.DllImport("/System/Library/Frameworks/Foundation.framework/Foundation")]
		public static extern IntPtr NSHomeDirectory();

		public static string ContainerDirectory {
			get {
				var val = ((Foundation.NSString)ObjCRuntime.Runtime.GetNSObject(NSHomeDirectory())).ToString ();

				if(val.Contains("Container"))
					return val;
				return Path.Combine (val, "Application Support/DataSaver");
			}
		}
		public static string BaseDir = ContainerDirectory;


		public static readonly string LibDir = Path.Combine(BaseDir, "Library/");

		public static readonly string ApplicationsPath =   "/Applications";
	}
}

