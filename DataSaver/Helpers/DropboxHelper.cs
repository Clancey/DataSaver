using System;

namespace DataSaver
{
	public class DropboxHelper : BaseHelper
	{
		public DropboxHelper ()
		{
			
			Title = "Dropbox";
		}

		public override async void Pause ()
		{
			if(IsSandboxed)
				await RunAutomatorScript("pauseDropbox.workflow");
			else
				await RunProcess("pkill","-stop  Dropbox");
		}

		public override async void Resume ()
		{

			if(IsSandboxed)
				await RunAutomatorScript("resumeDropbox.workflow");
			else
				await RunProcess("pkill","-cont  Dropbox");
		}
	}
}

