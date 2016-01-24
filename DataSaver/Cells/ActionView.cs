using System;
using AppKit;
using CoreGraphics;

namespace DataSaver
{
	public class ActionView : NSColorView
	{
		WeakReference _action;

		public ActionClass Action
		{
			get
			{
				return _action?.Target as ActionClass;
			}

			set
			{
				_action = new WeakReference(value);
				UpdateAction();
			}
		}

		bool editMode;
		public bool EditMode { 
			get { return editMode; }
			set 
			{
				editMode = value;
				CheckIfNeedsExtra();
				ResizeSubviewsWithOldSize(CGSize.Empty);
			}
		}

		CheckBox CheckBox;

		NSTextField NameLabel;
		NSTextField NameText;

		NSTextField PauseLabel;
		NSComboBox PauseType;
		NSTextField PauseText; 

		NSTextField ResumeLabel;
		NSComboBox ResumeType;
		NSTextField ResumeText;

		NSTextField AutomatorWarning;

		NSTextField ScriptPath;
		SimpleButton OpenFolderButton;

		SimpleButton SaveButton;
		SimpleButton CancelButton;
		bool HasExtra;
		public ActionView()
		{
			BackgroundColor = NSColor.LabelColor.ColorWithAlphaComponent(.15f);

			AddSubview(CheckBox = new CheckBox
			{
				Title = "Enabled",
				ValueChanged = (b) =>
				{
					Action.Enabled = b;
					//TODO: Save
				},
			});

			AddSubview(NameLabel = new NSTextField
			{
				StringValue = "Name",
			}.StyleAsLabel());

			AddSubview(NameText = new NSTextField
			{

			}.StyleAsLabel());

			AddSubview(PauseLabel = new NSTextField
			{
				StringValue = "Pause Action",
			}.StyleAsLabel());

			AddSubview(PauseType = new NSComboBox
			{
				Enabled = false,
			});

			AddSubview(PauseText = new NSTextField
			{

			}.StyleAsLabel());

			AddSubview(ResumeLabel = new NSTextField
			{
				StringValue = "Resume Action",
			}.StyleAsLabel());

			AddSubview(ResumeType = new NSComboBox
			{
				Enabled = false,
			});

			AddSubview(ResumeText = new NSTextField
			{

			}.StyleAsLabel());
		}

		void CheckIfNeedsExtra()
		{
			if (HasExtra)
				return;

			HasExtra = true;

			CheckBox.RemoveFromSuperview();
			NameLabel.RemoveFromSuperview();

			NameText.StyleAsTextEntry();

			PauseType.Enabled = true;
			PauseText.StyleAsTextEntry();

			ResumeType.Enabled = true;
			ResumeText.StyleAsTextEntry();


			AddSubview(SaveButton = new SimpleButton
			{
				Title = "Save",
				Tapped = (b)=>
				{
					//TODO: Save and dismiss
				}
			});

			AddSubview(CancelButton = new SimpleButton
			{
				Title = "Cancel",
				Tapped = (b)=>
				{
					//TODO: dismiss
					AppDelegate.CurrentWindow.EndSheet(this.Window);
				}
			});

			if (!App.IsSandboxed)
				return;

			AddSubview(AutomatorWarning = new NSTextField
			{
				StringValue = "* Automator scripts must be located inside the sripts folder:",
			}.StyleAsLabel());

			AddSubview(ScriptPath = new NSTextField
			{
				StringValue = App.GetScriptPath.Path,
			}.StyleAsLabel());

			AddSubview(OpenFolderButton = new SimpleButton
			{
				StringValue = "Show in Finder",
				Tapped = (b) =>
				{
					OpenInFinder();
				}
			});
		}


		public void UpdateAction()
		{
			CheckBox.Checked = Action?.Enabled ?? false;
			NameText.StringValue = Action?.Name ?? "";
			//PauseType = Action?.PauseCommandType
			PauseText.StringValue = Action?.PauseCommand ?? "";
			//ResumeType = Action?.ResumeCommandType
			ResumeText.StringValue = Action?.ResumeCommand ?? "";
		}

		void OpenInFinder()
		{
			NSWorkspace.SharedWorkspace.ActivateFileViewer(new[] { App.GetScriptPath });
		}

		protected const float Padding = 5f;
		protected const float TwicePadding = Padding * 2;
		protected const float ComboBoxWidth = 100f;
		const float durationWidth = 30f;
		const float ratingWidth = 50f;
		const float minTitleWidth = 200f;
		const float accessoryWidth = 30f;
		const float offlineIconWidth = 13f;

		public override bool IsFlipped
		{
			get
			{
				return true;
			}
		}

		public override void ResizeSubviewsWithOldSize(CoreGraphics.CGSize oldSize)
		{
			base.ResizeSubviewsWithOldSize(oldSize);

			CheckBox.SizeToFit();

			var bounds = Bounds;

			var frame = CheckBox.Frame;
			frame.X = Padding;
			frame.Y = Padding;
			CheckBox.Frame = frame;

			var x = EditMode ? Padding : frame.Right + Padding;
			var y = frame.Y;

			if (EditMode)
			{
				NameLabel.SizeToFit();
				frame = NameLabel.Frame;
				frame.X = x;
				frame.Y = y;
				x = frame.Right + Padding;
			}


			NameText.SizeToFit();
			frame = NameText.Frame;
			frame.Width = bounds.Width - x - Padding;
			frame.X = x;
			frame.Y = y;
			NameText.Frame = frame;

			x = Padding;
			y = frame.Bottom + TwicePadding;

			PauseLabel.SizeToFit();
			frame = PauseLabel.Frame;
			frame.X = x;
			frame.Y = y;
			PauseLabel.Frame = frame;

			y = frame.Bottom + Padding;

			PauseType.SizeToFit();
			frame = PauseType.Frame;
			frame.X = x;
			frame.Y = y;
			frame.Width = ComboBoxWidth;
			PauseType.Frame = frame;

			x = frame.Right + Padding;

			PauseText.SizeToFit();
			frame.X = x;
			frame.Y = y;
			frame.Width = bounds.Width - Padding - x;
			PauseText.Frame = frame;

			x = Padding;
			y = frame.Bottom + TwicePadding;

			ResumeLabel.SizeToFit();
			frame = ResumeLabel.Frame;
			frame.X = x;
			frame.Y = y;
			ResumeLabel.Frame = frame;

			y = frame.Bottom + Padding;

			ResumeType.SizeToFit();
			frame = ResumeType.Frame;
			frame.X = x;
			frame.Y = y;
			frame.Width = ComboBoxWidth;
			ResumeType.Frame = frame;

			x = frame.Right + Padding;

			ResumeText.SizeToFit();
			frame.X = x;
			frame.Y = y;
			frame.Width = bounds.Width - Padding - x;
			ResumeText.Frame = frame;

			y = frame.Bottom + TwicePadding;
			x = Padding;

			if (!EditMode)
				return;
			if (App.IsSandboxed)
			{
				y -= Padding;
				AutomatorWarning.SizeToFit();
				frame = AutomatorWarning.Frame;
				frame.X = x;
				frame.Y = y;
				AutomatorWarning.Frame = frame;

				OpenFolderButton.SizeToFit();

				y = frame.Bottom + Padding;
				ScriptPath.SizeToFit();
				frame = ScriptPath.Frame;
				frame.Width = bounds.Width - TwicePadding - x - OpenFolderButton.Frame.Width;
				frame.Y = y;
				frame.X = x;
				ScriptPath.Frame = frame;

				x = frame.Right + Padding;
				frame = OpenFolderButton.Frame;
				frame.X = x;
				frame.Y = y;
				OpenFolderButton.Frame = frame;

				y = frame.Bottom + TwicePadding;
				x = Padding;
			}

			var midx = bounds.Width / 2;

			SaveButton.SizeToFit();
			CancelButton.SizeToFit();

			frame = SaveButton.Frame;
			frame.Y = y;
			frame.X = midx + TwicePadding;
			SaveButton.Frame = frame;

			frame = CancelButton.Frame;
			frame.Y = y;
			frame.X = midx - TwicePadding - frame.Width;
			CancelButton.Frame = frame;
		}

	}
}

