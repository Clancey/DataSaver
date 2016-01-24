using System;
using System.Collections.Generic;
using Xamarin.Tables;
namespace DataSaver
{
	public class ActionsViewModel : TableViewModel<ActionClass>
	{
		public List<ActionClass> Actions = new List<ActionClass>
		{
			new ActionClass{
				Enabled = true,
				Name = "Dropbox",
				PauseCommandType = ActionType.AutomatorScript,
				PauseCommand = "foo",
				ResumeCommand = "foo",
				ResumeCommandType = ActionType.AutomatorScript,
			},
			new ActionClass{

				Enabled = true,
				Name = "Dropbox",
				PauseCommandType = ActionType.Command,
				PauseCommand = "foo",
				ResumeCommand = "foo",
				ResumeCommandType = ActionType.Command,
			},
		};
		public ActionsViewModel()
		{
		}
		public override string HeaderForSection(int section)
		{
			return "";
		}

		public override ActionClass ItemFor(int section, int row)
		{
			return Actions[row];
		}

		public override int NumberOfSections()
		{
			return 1;
		}

		public override int RowsInSection(int section)
		{
			return Actions.Count;
		}
		public override ICell GetICell(int section, int row)
		{
			var item = ItemFor(section, row);
			return new ActionCell
			{
				Action = item,
			};
		}

		public Action SelectionsChanged { get; set; }
		public override void SelectionDidChange(Foundation.NSNotification notification)
		{
			SelectionsChanged?.Invoke();
			base.SelectionDidChange(notification);
		}
	}
}

