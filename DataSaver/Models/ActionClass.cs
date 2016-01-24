using System;
namespace DataSaver
{

	public enum ActionType
	{
		BashScript,
		Command,
		AutomatorScript,
	}
	public class ActionClass
	{
		public bool Enabled { get; set; }

		public string Name { get; set; }

		public ActionType PauseCommandType { get; set; } = ActionType.AutomatorScript;

		public string PauseCommand { get; set; }

		public ActionType ResumeCommandType { get; set; } = ActionType.AutomatorScript;

		public string ResumeCommand { get; set; }

	}
}

