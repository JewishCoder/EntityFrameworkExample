using DataStorage.Entities;
using DataStorage.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataStorage.Repositories
{
	public class ServerIpV4Filter
	{
		public string HostName { get; set; }
		
		public string Ip { get; set; }
		
		public NullValue<int> Port { get; set; }

		internal Expression<Func<ServerIpV4, bool>> CreatePredicate() 
		{
			var builder = new FilterBuilder<ServerIpV4>();
			var predicate = default(Expression<Func<ServerIpV4, bool>>);
			if(!string.IsNullOrWhiteSpace(HostName)) 
			{
				predicate = builder.Build(x => x.HostName.Equals(HostName));
			}
			if(!string.IsNullOrWhiteSpace(Ip)) 
			{
				predicate = builder.Build(x => x.IpV4.Equals(Ip));
			}
			if(Port != null) 
			{
				if(Port.Value.HasValue)
				{
					predicate = builder.Build(x => x.PortV4.Equals(Port.Value.Value));
				}
				else 
				{
					predicate = builder.Build(x => x.PortV4 == null);
				}
			}

			return predicate;
		}
	}
}
