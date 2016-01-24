using System;
using System.Diagnostics;
using Foundation;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace DataSaver
{
	public abstract class BaseHelper
	{
		public BaseHelper()
		{
			IsEnabled = true;
		}
		public string Title { get; set; }

		public abstract void Pause ();

		public abstract void Resume ();

		public virtual bool IsEnabled { get; set; }

		public virtual bool IsInstalled { get; set; }

		public static bool IsSandboxed
		{
			get{
				return App.IsSandboxed;
			}
		}

		public async Task<bool> RunProcess (string file, string args)
		{
			try{
				Process proc = new System.Diagnostics.Process ();
				proc.StartInfo.FileName = file;
				proc.StartInfo.Arguments = args;
				proc.StartInfo.UseShellExecute = false; 
				proc.StartInfo.RedirectStandardOutput = true;
				proc.Start ();
				await Task.Run (proc.WaitForExit);
				return true;
			}
			catch(Exception ex) {
				Console.WriteLine (ex);
			}
			return false;
		}

		public Task<bool> RunBash (string sh)
		{
			var s = File.Exists(sh) ? sh : Path.Combine (NSBundle.MainBundle.ResourcePath, sh);
			return RunProcess("/bin/bash",$"-c {s}");
		}

		public Task<bool> RunAutomatorScript(string script)
		{
			var tcs = new TaskCompletionSource<bool> ();
			try{
				var url = GetAutomatorUri(script);
				var task = new NSUserAutomatorTask (url, null);// as NSUserAutomatorTask;
				task.Execute (null, ((result, error) => {
					tcs.TrySetResult(error == null);
				}));
			}
			catch(Exception ex) {
				tcs.TrySetException (ex);
			}
			return tcs.Task;
		}

		public static bool AutomatorFileExists(string script)
		{
			try{
				var url = GetAutomatorUri(script);
				var task = new NSUserAutomatorTask (url, null);// as NSUserAutomatorTask;
				return true;
			}
			catch(Exception ex) {
			}
			return false;
		}

		static NSUrl GetAutomatorUri(string script)
		{

			var baseUrls = NSFileManager.DefaultManager.GetUrls (NSSearchPathDirectory.ApplicationScriptsDirectory, NSSearchPathDomain.All).FirstOrDefault();
			var url = new NSUrl (script, baseUrls);
			return url;
		}
	}
}

