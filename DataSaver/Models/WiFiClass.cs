using System;
using SQLite;

namespace DataSaver
{
	public class WiFiClass
	{
		public bool Enabled { get; set; } = true;

		[PrimaryKey]
		public string SSID { get; set; }
	}
}

