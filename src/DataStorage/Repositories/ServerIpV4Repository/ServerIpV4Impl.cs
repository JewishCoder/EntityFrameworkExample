using DataStorage.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorage.Repositories
{
	internal class ServerIpV4Impl : IEntity, IServerIpV4
	{
		public long Id { get; }
		
		public string HostName { get; }
		
		public string Ip { get; }
		
		public int? Port { get; }

		public ServerIpV4Impl(string hostName, string ip, int? port)
		{
			Id = -1;
			HostName = hostName;
			Ip = ip;
			Port = port;
		}
	}
}
