using System;
using AppKit;
namespace DataSaver
{
	public class CheckBox : NSButton
	{
		public Action<bool> ValueChanged { get; set; }
		public CheckBox()
		{
			this.AllowsMixedState = false;
			this.SetButtonType(NSButtonType.Switch);
			this.Activated += (object sender, EventArgs e) =>
			{
				ValueChanged?.Invoke(Checked);
			};
			this.Title = "";
		}

		public bool Checked
		{
			get { return this.State == NSCellStateValue.On; }
			set { 
				this.State = value ? NSCellStateValue.On : NSCellStateValue.Off; 
			}
		}
	}
}

