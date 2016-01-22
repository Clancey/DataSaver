using System;
using System.Runtime.InteropServices;
using ObjCRuntime;
using Foundation;
using System.Runtime.CompilerServices;

namespace DataSaver
{
	public class Settings
	{

		static Plugin.Settings.Abstractions.ISettings AppSettings { get; } = Plugin.Settings.CrossSettings.Current;
		public static Settings Shared {get;set;} = new Settings();




		[DllImport("/System/Library/Frameworks/ServiceManagement.framework/ServiceManagement")]
		static extern bool SMLoginItemSetEnabled(IntPtr aId, bool aEnabled);


		[DllImport("/System/Library/Frameworks/ServiceManagement.framework/ServiceManagement")]
		static extern IntPtr SMJobCopyDictionary(IntPtr aDomain, IntPtr aId);		


		public bool StartAtLogin {
			get {
				try{
					return GetBool();
				}
				catch(Exception ex) {
					Console.WriteLine (ex);
					return false;
				}
			}
			set {
				SetBool (value);
				CoreFoundation.CFString id = new CoreFoundation.CFString("com.iis.DataSaver");
				SMLoginItemSetEnabled(id.Handle, value);
			}
		}

		static string GetString(string defaultValue = "",[CallerMemberName] string memberName = "")
		{
			return AppSettings.GetValueOrDefault(memberName,defaultValue);
		}

		static void SetString (string value,[CallerMemberName] string memberName = "")
		{
			AppSettings.AddOrUpdateValue<string>(memberName, value);
		}
		static int GetInt(int defaultValue = 0, [CallerMemberName] string memberName = "")
		{
			return AppSettings.GetValueOrDefault(memberName, defaultValue);
		}

		static void SetInt(int value, [CallerMemberName] string memberName = "")
		{
			AppSettings.AddOrUpdateValue<int>(memberName, value);
		}

		static long GetLong(long defaultValue = 0, [CallerMemberName] string memberName = "")
		{
			return AppSettings.GetValueOrDefault(memberName, defaultValue);
		}

		static void SetLong(long value, [CallerMemberName] string memberName = "")
		{
			AppSettings.AddOrUpdateValue<long>(memberName, value);
		}

		static bool GetBool(bool defaultValue = false, [CallerMemberName] string memberName = "")
		{
			return AppSettings.GetValueOrDefault(memberName, defaultValue);
		}

		static void SetBool(bool value, [CallerMemberName] string memberName = "")
		{
			AppSettings.AddOrUpdateValue<bool>(memberName, value);
		}


		static T Get<T>(T defaultValue, [CallerMemberName] string memberName = "")
		{
			return AppSettings.GetValueOrDefault(memberName, defaultValue);
		}

		static void Set<T>(T value, [CallerMemberName] string memberName = "")
		{
			AppSettings.AddOrUpdateValue<T>(memberName, value);
		}
	}
}

