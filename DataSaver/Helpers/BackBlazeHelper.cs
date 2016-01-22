using System;
using System.IO;

namespace DataSaver
{
	public class BackBlazeHelper : BaseHelper
	{
		public BackBlazeHelper()
		{
			Title = "Backblaze";
		}
		public override bool IsInstalled {
			get {
				return File.Exists (FilePath);
			}
			set {
				base.IsInstalled = value;
			}
		}

		const string FilePath  = "/Library/Backblaze.bzpkg/bztransmit";

		public override void Pause()
		{
			RunProcess (FilePath,"-pausebackup");
		}

		public override void Resume()
		{
			RunProcess (FilePath,"-completesync");
		}

	}
}

