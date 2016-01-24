using System;
using AppKit;
using Foundation;
using Xamarin.Tables;
namespace DataSaver
{
	public class ActionCell : ICell
	{
		public ActionClass Action { get; set; }
		public ActionCell()
		{
		}

		NSView ICell.GetCell(NSTableView tableView, NSTableColumn tableColumn, NSObject owner)
		{
			var cell = tableView.MakeView("ActionView", owner) as ActionView  ?? new ActionView();
			cell.Action = Action;
			return cell;
		}

		string ICell.GetCellText(NSTableColumn tableColumn)
		{
			throw new NotImplementedException();
		}

	}
}

