using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorage.Repositories
{
	public sealed class ServerIpV4Data : IServerIpV4
	{
		public string HostName { get; }
		
		public string Ip { get; }
		
		public int? Port { get; }

		public ServerIpV4Data(string hostName, string ip, int? port = null)
		{
			HostName = hostName;
			Ip       = ip;
			Port     = port;
		}
	}
}
