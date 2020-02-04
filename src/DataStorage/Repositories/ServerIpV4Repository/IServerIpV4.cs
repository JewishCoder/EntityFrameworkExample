using DataStorage.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorage.Repositories
{
	public interface IServerIpV4
	{
		string HostName { get; }

		string Ip { get; }

		int? Port { get; }
	}
}
