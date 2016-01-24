using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Tables;
namespace DataSaver
{
	public class ActionsViewModel : TableViewModel<ActionClass>
	{
		public static ActionClass[] DefaultsAction = new ActionClass[]
		{
			new ActionClass{
				Enabled = true,
				Name = "Dropbox",
				PauseCommandType = ActionType.Dropbox,
				ResumeCommandType = ActionType.Dropbox,
			},
			new ActionClass{

				Enabled = true,
				Name = "Backblaze",
				PauseCommandType = ActionType.Backblaze,
				ResumeCommandType = ActionType.Backblaze,
			},
		};
		public List<ActionClass> Actions = new List<ActionClass>
		{
			
		};
		public ActionsViewModel()
		{
			Actions = Database.Main.Table<ActionClass>().ToList();
			foreach (var action in DefaultsAction)
			{
				var exists = Actions.Any(x => x.PauseCommandType == action.PauseCommandType);
				if (exists)
					continue;

				var helper = BaseHelper.CreateHelper(action, true);
				if (!helper.IsInstalled)
					continue;
				
				Database.Main.Insert(action);
				Actions.Add(action);
			}
		}

		public void Add(ActionClass wifi)
		{
			if (wifi.Id == 0)
				Database.Main.Insert(wifi);
			else
				Database.Main.InsertOrReplace(wifi);
			Actions = Database.Main.Table<ActionClass>().ToList();
			ReloadData();
		}

		public void Save(ActionClass wifi)
		{
			Add(wifi);
		}

		public void Delete(int index)
		{
			try
			{
				Delete(Actions[index]);
			}
			catch (Exception ex)
			{
				Xamarin.Insights.Report(ex);
			}
		}

		public void Delete(ActionClass wifi)
		{
			Database.Main.Delete(wifi);
			Actions = Database.Main.Table<ActionClass>().ToList();
			ReloadData();
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

