using System;
using AppKit;

namespace DataSaver
{
	public static class Style
	{
		public static T StyleAsLabel<T>(this T label) where T : NSTextField
		{
			label.Enabled = false;
			label.Bordered = false;
			return label;
		}
		public static T StyleAsTextEntry<T>(this T text) where T : NSTextField
		{
			text.Enabled = true;
			text.Bordered = true;
			return text;
		}
	}
}

