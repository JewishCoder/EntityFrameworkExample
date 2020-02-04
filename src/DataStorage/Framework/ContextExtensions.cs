using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorage
{
	public static class ContextExtensions
	{
		public static void ConfigureAsFetchOnly(this Context context) 
		{
			context.Configuration.AutoDetectChangesEnabled = false;
			context.Configuration.LazyLoadingEnabled = false;
			context.Configuration.ProxyCreationEnabled = false;
		}
	}
}
