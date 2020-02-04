using DataStorage.Entities;
using DataStorage.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataStorage.Repositories
{
	internal class ServerIpV4Repository :
		Framework.BaseRepository
		<
			IServerIpV4,
			ServerIpV4,
			ServerIpV4Filter
		>, 
		IServerIpV4Repository
	{
		public ServerIpV4Repository(IConnectionFactory connectionFactory) : base(connectionFactory)
		{

		}

		protected override IQueryable<ServerIpV4> ApplyFilter(IQueryable<ServerIpV4> query, ServerIpV4Filter filter)
		{
			if(filter == null) return query;

			var predicate = filter.CreatePredicate();
			if(predicate != null) 
			{
				query = query.Where(predicate);
			}

			return query;
		}

		protected override IQueryable<ServerIpV4> ApplyInclude(IQueryable<ServerIpV4> query)
		{
			return query;
		}

		protected override ServerIpV4 CreateEntity(IServerIpV4 data)
		{
			return new ServerIpV4
			{
				HostName = data.HostName,
				IpV4 = data.Ip,
				PortV4 = data.Port,
			};
		}

		protected override Task<ServerIpV4> CreateEntityAsync(IServerIpV4 data, CancellationToken cancellationToken)
		{
			return Task.Run(() => CreateEntity(data), cancellationToken);
		}

		protected override IServerIpV4 Represent(ServerIpV4 entity)
		{
			return new ServerIpV4Impl(entity.HostName, entity.IpV4, entity.PortV4);
		}
	}
}
