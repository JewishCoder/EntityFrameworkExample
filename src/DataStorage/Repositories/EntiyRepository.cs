using DataStorage.Entities;
using DataStorage.Providers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace DataStorage.Repositories
{
	public sealed class EntiyRepository
	{
		private const int RemoveLimit = 100;

		private readonly SqlServerConnectionFactory _connectionFactory;

		public EntiyRepository()
		{
			_connectionFactory = new SqlServerConnectionFactory();
		}

		public IReadOnlyList<TEntity> Fetch<TEntity>() where TEntity : class 
		{
			using(var context = new Context(_connectionFactory.CreateConnection()))
			{
				context.ConfigureAsFetchOnly();

				return context.Set<TEntity>().AsNoTracking().ToList();
			}
		}

		public async Task<IReadOnlyList<TEntity>> FetchAsync<TEntity>(CancellationToken cancellationToken) where TEntity : class
		{
			using(var context = new Context(_connectionFactory.CreateConnection()))
			{
				context.ConfigureAsFetchOnly();

				return await context
					.Set<TEntity>()
					.AsNoTracking()
					.ToListAsync(cancellationToken)
					.ConfigureAwait(continueOnCapturedContext: false);
			}
		}

		public IReadOnlyList<VideoChannel> FetchVideoChannels()
		{
			using(var context = new Context(_connectionFactory.CreateConnection()))
			{
				context.ConfigureAsFetchOnly();

				return context
					.Set<VideoChannel>()
					.Include(x => x.Server)
					.Include(x => x.Server.Metadata)
					.AsNoTracking()
					.ToList();
			}
		}

		public int AddRange<TEntity>(IReadOnlyList<TEntity> entities) 
			where TEntity : class
		{
			if(entities.Count == 0) return 0;

			using(var transactionScope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
			using(var context = new Context(_connectionFactory.CreateConnection()))
			{
				context.Set<TEntity>().AddRange(entities);

				var addedCount = context.SaveChanges();

				transactionScope.Complete();

				return addedCount;
			}
		}

		public int RemoveByFilter<TEntity>(Expression<Func<TEntity, bool>> filter) 
			where TEntity : class
		{
			if(filter == null) return 0;

			using(var context = new Context(_connectionFactory.CreateConnection()))
			{
				var query = context.Set<TEntity>().Where(filter);
				var recordCount = query.Count();
				if(recordCount > RemoveLimit)
				{
					var removeWindowCount = Math.Round(recordCount * 1.0d / RemoveLimit);
					for(var i = 0; i < removeWindowCount; i++)
					{
						var skip = i * RemoveLimit;
						var records = query
							.Skip(() => skip)
							.Take(() => RemoveLimit)
							.ToArray();

						context.Set<TEntity>().RemoveRange(records);
					}
				}
				else 
				{
					var records = query.ToArray();
					context.Set<TEntity>().RemoveRange(records);
				}

				return context.SaveChanges();
			}
		}

		public async Task<int> RemoveByFilterAsync<TEntity>(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken)
			where TEntity : class
		{
			if(filter == null) return 0;

			using(var context = new Context(_connectionFactory.CreateConnection()))
			{
				var query = context.Set<TEntity>().Where(filter);

				var recordCount = await query.CountAsync()
					.ConfigureAwait(continueOnCapturedContext: false);
				if(recordCount > RemoveLimit)
				{
					var removeWindowCount = Math.Round(recordCount * 1.0d / RemoveLimit);
					for(var i = 0; i < removeWindowCount; i++)
					{
						var skip = i * RemoveLimit;
						var records = await query
							.Skip(() => skip)
							.Take(() => RemoveLimit)
							.ToArrayAsync(cancellationToken)
							.ConfigureAwait(continueOnCapturedContext: false);

						context.Set<TEntity>().RemoveRange(records);
					}
				}
				else
				{
					var records = await query
						.ToArrayAsync(cancellationToken)
						.ConfigureAwait(continueOnCapturedContext: false);
					context.Set<TEntity>().RemoveRange(records);
				}

				return await context
					.SaveChangesAsync(cancellationToken)
					.ConfigureAwait(continueOnCapturedContext: false);
			}
		}

	}
}
