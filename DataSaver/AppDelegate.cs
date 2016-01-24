using AppKit;
using Foundation;
using Connectivity.Plugin;
using CoreGraphics;
using System.Threading.Tasks;

namespace DataSaver
{
	[Register ("AppDelegate")]
	public class AppDelegate : NSApplicationDelegate
	{
		public static NSWindow CurrentWindow { get; private set; }
		public AppDelegate ()
		{
			
		}

		public override void DidFinishLaunching (NSNotification notification)
		{
			App.CheckStatus();
		}

		void CrossConnectivity_Current_ConnectivityChanged (object sender, Connectivity.Plugin.Abstractions.ConnectivityChangedEventArgs e)
		{
			App.CheckStatus();
		}

		public override void WillTerminate (NSNotification notification)
		{
			// Insert code here to tear down your application
			StateManager.Resume ();
		}
		NSStatusItem item;
		NSImage onImage;
		NSImage offImage;
		NSMenuItem startStop;
		NSMenuItem pauseUnpause;
		StatusItemView statusView;
		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();

			CrossConnectivity.Current.ConnectivityChanged += CrossConnectivity_Current_ConnectivityChanged;
			item = NSStatusBar.SystemStatusBar.CreateStatusItem (NSStatusItemLength.Variable);
			item.Menu = new NSMenu ("DataSaver");
			item.Image =offImage = NSBundle.MainBundle.ImageForResource ("statusBarNormal");
			onImage = NSBundle.MainBundle.ImageForResource ("statusBarConnected");
			item.HighlightMode = true;

			item.Menu.AddItem (pauseUnpause = new NSMenuItem ("Pause Syncing", (s, e) => {
				StateManager.TogglePaused();
			}));

			item.Menu.AddItem (startStop = new NSMenuItem ("Disable", (s, e) => {
				StateManager.ToggleEnabled();
			}));

			item.Menu.AddItem (new NSMenuItem ("Settings", (s, e) => {
				ShowSettings();
			}));

			item.Menu.AddItem (new NSMenuItem ("Quit", (s, e) => {
				NSApplication.SharedApplication.Terminate(this);
			}));
			item.View = statusView = new StatusItemView (item);
			StateManager.StateChanged = SetState;
			SetState ();

		}

		void SetState()
		{
			pauseUnpause.Title = StateManager.IsPaused ? "Resume Syncing" : "Pause Syncing";
			startStop.Title = StateManager.IsEnabled ? "Disable" : "Enable";
			statusView.IsConnected = StateManager.IsPaused;
			AnimateImageState (StateManager.IsPaused);
		}
		void AnimateImageState(bool on)
		{
			NSAnimationContext.RunAnimation ((context) => {
				context.Duration = 2.0;
				item.Image = on ? onImage : offImage;
			},null);
		}

		void ShowSettings()
		{
			if (CurrentWindow == null)
			{
				var storyboard = NSStoryboard.FromName("Main", null);
				var controller = storyboard.InstantiateControllerWithIdentifier("SettingsWindow") as NSWindowController;

				var window = controller.Window;
				var frame = window.Frame;
				CurrentWindow = window;
			}
			CurrentWindow.MakeKeyAndOrderFront(null);
			CurrentWindow.Level = NSWindowLevel.Status;
//			var eventFrame = this.applica [[[NSApp currentEvent] window] frame];
//			var eventOrigin = eventFrame.origin;
//			var eventSize = eventFrame.size;
//
//			var windowSize = frame.Size;
//			var windowTopLeftPosition =  new CGPoint(eventOrigin.X + eventSize.Width/2f - windowSize.Width/2f, eventOrigin.Y - 20);
//
//			// Set position of the window and display it
//			[window setFrameTopLeftPoint:windowTopLeftPosition];
			// Display
			//controller.ShowWindow(this);

		}

	}
}

