using System;
using AppKit;
namespace DataSaver
{
	public class SimpleButton : NSButton
	{
		public Action<NSButton> Tapped { get; set; }
		public SimpleButton()
		{
			Activated += (object sender, EventArgs e) => { Tapped?.Invoke(this);};
			this.BezelStyle = NSBezelStyle.RoundRect;
		}
	}
}

