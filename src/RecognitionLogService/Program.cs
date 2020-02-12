using DataStorage;
using DataStorage.Entities;
using DataStorage.Providers;
using DataStorage.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RecognitionLogService
{
	class Program
	{
		static async Task Main(string[] args)
		{
			try
			{
				var host = Dns.GetHostName();
				var ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList.First(x => x.AddressFamily == AddressFamily.InterNetwork).ToString();

				await DatabaseInitializationService.InitializeAsync(default);

				var adoRepository = new AdoNetRepository();
				adoRepository.AddServerMetadata(new[] { new ServerMetadata { HostName = Dns.GetHostName() } });
				var data = adoRepository.FetchMetaData();

				var entityRepository = new EntiyRepository();
				entityRepository.AddRange(new[]
				{
					new ServerIpV4
					{
						HostName = host,
						IpV4 = ip,
					},
				});

				var serverMetadata = entityRepository.Fetch<ServerIpV4>();
				var removed = entityRepository.RemoveByFilter<ServerIpV4>(x => x.HostName == host);

				var connectionFactory = new SqlServerConnectionFactory();
				var repFactory = new RepositoryFactory(connectionFactory);
				var serverIpV4Repository = repFactory.GetRepository<IServerIpV4Repository>();
				
			

				serverIpV4Repository.AddRange(new[] { new ServerIpV4Data(host, ip) });
				var added = serverIpV4Repository.FetchRecords();
				removed = serverIpV4Repository.RemoveByFilter(new ServerIpV4Filter { HostName = host });
			}
			catch(Exception exc)
			{
				Console.WriteLine(exc.ToString());
			}
			finally 
			{
				Console.ReadKey();
			}
		}
	}
}
