using System;
using System.Collections.Generic;
using System.IO;
namespace DataSaver
{
	public class Database : SQLite.SQLiteConnection
	{
		public static Database Main { get; set; } = new Database(Path.Combine(Locations.LibDir, "settings.db"));

		public Database(string file) : base(file, true)
		{
			this.CreateTable<WiFiClass>();
			this.CreateTable<ActionClass>();
		}
	}
}

