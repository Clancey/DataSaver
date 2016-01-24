using System;
using System.Collections.Generic;
using AppKit;
using CoreGraphics;
using Foundation;

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
			BackgroundColor = NSColor.LightGray.ColorWithAlphaComponent(.1f);
			Layer = new CoreAnimation.CALayer
			{
				BorderColor = NSColor.DarkGray.CGColor,
				BorderWidth = 1f,
				CornerRadius = 5f,
			};

			AddSubview(CheckBox = new CheckBox
			{
				Title = "Enabled",
				ValueChanged = (b) =>
				{
					Action.Enabled = b;
					if(!EditMode)
						App.ActionsViewModel.Save(Action);
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
			PauseType.Add(ActionTypes);

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
				DataSource = new DropdownSource(),
			});

			ResumeType.Add(ActionTypes);

			AddSubview(ResumeText = new NSTextField
			{

			}.StyleAsLabel());
		}
		static NSString[] ActionTypes = new []
		{
			(NSString)"BashScript",
			(NSString)"Command",
			(NSString)"AutomatorScript",
		};
		class DropdownSource : NSComboBoxDataSource
		{
			public List<string> ActionTypes = new List<string>();
			public DropdownSource()
			{
				
			}
			public override string CompletedString(NSComboBox comboBox, string uncompletedString)
			{
				return ActionTypes.Find(n => n.StartsWith(uncompletedString, StringComparison.InvariantCultureIgnoreCase));
			}

			public override nint IndexOfItem(NSComboBox comboBox, string value)
			{
				return ActionTypes.FindIndex(n => n.Equals(value, StringComparison.InvariantCultureIgnoreCase));
			}

			public override nint ItemCount(NSComboBox comboBox)
			{
				return ActionTypes.Count;
			}

			public override NSObject ObjectValueForItem(NSComboBox comboBox, nint index)
			{
				return NSObject.FromObject(ActionTypes[(int)index]);
			}
		}

		void CheckIfNeedsExtra()
		{
			if (HasExtra)
				return;

			HasExtra = true;

			CheckBox.RemoveFromSuperview();

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
					App.ActionsViewModel.Save(Action);
					AppDelegate.CurrentWindow.EndSheet(this.Window);
				}
			});

			AddSubview(CancelButton = new SimpleButton
			{
				Title = "Cancel",
				Tapped = (b)=>
				{
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
			PauseType.Select(new NSString(Action?.PauseCommandType.ToString()));
			PauseText.StringValue = Action?.PauseCommand ?? "";
			ResumeType.Select(new NSString(Action?.ResumeCommandType.ToString()));
			ResumeText.StringValue = Action?.ResumeCommand ?? "";

			bool standard = Action.PauseCommandType == ActionType.Backblaze || Action.PauseCommandType == ActionType.Dropbox;
			PauseLabel.Hidden = standard;
			PauseType.Hidden = standard;
			PauseType.Hidden = standard;
			ResumeLabel.Hidden = standard;
			ResumeType.Hidden = standard;
			ResumeType.Hidden = standard;

		}

		void OpenInFinder()
		{
			NSWorkspace.SharedWorkspace.ActivateFileViewer(new[] { App.GetScriptPath });
		}

		protected const float Padding = 5f;
		protected const float TwicePadding = Padding * 2;
		protected const float ComboBoxWidth = 150f;
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
			Layer.Frame = bounds;
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
				frame.X = Padding;
				frame.Y = Padding;
				NameLabel.Frame = frame;
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

