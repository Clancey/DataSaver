using System;
using SQLite;
namespace DataSaver
{

	public enum ActionType
	{
		BashScript,
		Command,
		AutomatorScript,
		Dropbox,
		Backblaze,
	}
	public class ActionClass
	{
		[PrimaryKeyAttribute, AutoIncrement]
		public int Id { get; set; }

		public bool Enabled { get; set; }

		public string Name { get; set; }

		public ActionType PauseCommandType { get; set; } = ActionType.AutomatorScript;

		public string PauseCommand { get; set; }

		public ActionType ResumeCommandType { get; set; } = ActionType.AutomatorScript;

		public string ResumeCommand { get; set; }

	}
}

