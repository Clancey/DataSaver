using System;
using AppKit;
using CoreGraphics;
using Foundation;
using System.Threading.Tasks;
using CoreAnimation;

namespace DataSaver
{
	public class StatusItemView : NSView
	{

		NSImageView normalImage;
		NSImageView connectedImage;
		public StatusItemView (IntPtr ptr) : base (ptr)
		{

		}
		public override void MouseDown (NSEvent theEvent)
		{
			base.MouseDown (theEvent);

			item.PopUpStatusItemMenu (item.Menu);
		}
		NSStatusItem item;
		public StatusItemView(NSStatusItem item) : base(new CGRect(0,0,NSStatusBar.SystemStatusBar.Thickness,item.Length == -1? NSStatusBar.SystemStatusBar.Thickness : item.Length))
		{
			this.item = item;
			//item.View = this;
			var size = new CGRect (0, 0, NSStatusBar.SystemStatusBar.Thickness, item.Length == -1? NSStatusBar.SystemStatusBar.Thickness : item.Length);
			AddSubview(normalImage = new NSImageView(size){Image = NSBundle.MainBundle.ImageForResource ("statusBarNormal")});
			AddSubview(connectedImage = new NSImageView (size) {
				Image = NSBundle.MainBundle.ImageForResource ("statusBarConnected"),
				AlphaValue = 0,
			});
			//startPulse ();
		}
		async void startPulse()
		{
			Task.Delay (100);
			Pulse (true);
		}
		public override bool IsFlipped {
			get {
				return true;
			}
		}
		bool isConnected;
		public bool IsConnected {
			get {
				return isConnected;
			}
			set {
				if (isConnected == value)
					return;
				isConnected = value;
				setState (value);
			}
		}

		async Task setState(bool connected)
		{
			var tcs = new TaskCompletionSource<bool> ();
			NSAnimationContext.BeginGrouping ();
			var context = NSAnimationContext.CurrentContext;
			context.TimingFunction = CAMediaTimingFunction.FromName (CAMediaTimingFunction.EaseInEaseOut);
			context.Duration = .5;
			var addImage = connected ? connectedImage : normalImage;
			var removeImage = connected ? normalImage : connectedImage;
			context.CompletionHandler = () => {
				tcs.TrySetResult(true);
				//removeImage.RemoveFromSuperview();
			};
			((NSView)addImage.Animator).AlphaValue = 1;
			((NSView)removeImage.Animator).AlphaValue = 0;
			NSAnimationContext.EndGrouping ();
			await tcs.Task;
		}

		public async void Pulse(bool on)
		{
			await setState(on);
			Pulse (!on);
		}
		public override void Layout ()
		{
			base.Layout ();
			connectedImage.Frame = normalImage.Frame = Bounds;
		}

	}
}