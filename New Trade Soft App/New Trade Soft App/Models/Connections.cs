namespace New_Trade_Soft_App.Models
{
	using mtapi.mt5;
	using System.Collections.Generic;

	public class Connections
    {
		public string Username { get; set; }
		public string Password { get; set; }
		public string Address { get; set; }
		public bool Connected { get; set; }

		public static List<string> ServersDat()
		{
			List<string> list = new List<string>();
            Server[] servers = MT5API.LoadServersDat("servers.dat");
			foreach (Server server in servers)
			{
				if (server.ServerInfo != null)
					list.Add(server.ServerInfo.ServerName);
				if (server.ServerInfoEx != null)
					list.Add(server.ServerInfoEx.ServerName);
				foreach (var access in server.Accesses)
					foreach (var addr in access.Addresses)
						list.Add(addr.Address);
			}

			return list;
		}
	}
}
