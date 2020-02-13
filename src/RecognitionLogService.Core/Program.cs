using DataStorage.Providers;
using RecognitionLogService.Core.Framework;
using System;

namespace RecognitionLogService.Core
{
	class Program
	{
		static void Main(string[] args)
		{
			var factory = new SqlServerConnectionFactory();
			var context = new StorageContext(factory.CreateConnection());
			context.Database.EnsureCreated();

			Console.WriteLine("Hello World!");
		}
	}
}
