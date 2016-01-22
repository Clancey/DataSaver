using AppKit;
using Xamarin;

namespace DataSaver
{
	static class MainClass
	{
		static void Main (string[] args)
		{
			Insights.Initialize("d5652b9cd0c928475e40407d4448a94429a9c033","1.0","com.iis.DataSaver");
			NSApplication.Init ();
			NSApplication.Main (args);
		}
	}
}
