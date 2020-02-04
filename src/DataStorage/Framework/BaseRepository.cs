using DataStorage.Entities;
using DataStorage.Providers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataStorage.Framework
{
	internal abstract class BaseRepository<TIEntity, TEntity, TFilter>
		where TIEntity : class
		where TEntity : class, IEntity
		where TFilter : class
	{
		protected virtual int RemoveLimit { get; }

		protected IConnectionFactory ConnectionFactory { get; }

		public BaseRepository(IConnectionFactory connectionFactory)
		{
			RemoveLimit = 100;
			ConnectionFactory = connectionFactory;
		}

		protected abstract IQueryable<TEntity> ApplyFilter(IQueryable<TEntity> query, TFilter filter);

		protected abstract IQueryable<TEntity> ApplyInclude(IQueryable<TEntity> query);

		protected abstract TEntity CreateEntity(TIEntity data);

		protected abstract Task<TEntity> CreateEntityAsync(TIEntity data, CancellationToken cancellationToken);

		protected abstract TIEntity Represent(TEntity entity);

		protected virtual DbSet<TEntity> GetDbSet(Context context) 
		{
			return context.Set<TEntity>();
		}

		public virtual IReadOnlyList<TIEntity> FetchRecords(TFilter filter = null) 
		{
			using(var context = CreateContext())
			{
				context.ConfigureAsFetchOnly();

				var query = GetDbSet(context).AsNoTracking().AsQueryable();
				query = ApplyFilter(query, filter);
				query = ApplyInclude(query);

				return query.ToList().ConvertAll(Represent);
			}
		}

		public virtual async Task<IReadOnlyList<TIEntity>> FetchRecordsAsync(TFilter filter = null, CancellationToken cancellationToken = default)
		{
			using(var context = await CreateContexAsync(cancellationToken)
				.ConfigureAwait(continueOnCapturedContext: false))
			{
				context.ConfigureAsFetchOnly();

				var query = GetDbSet(context).AsNoTracking().AsQueryable();
				query = ApplyFilter(query, filter);
				query = ApplyInclude(query);

				var records = await query
					.ToListAsync(cancellationToken)
					.ConfigureAwait(continueOnCapturedContext: false);

				return records.ConvertAll(Represent);
			}
		}

		public virtual int AddRange(IReadOnlyList<TIEntity> entities)
		{
			using(var context = CreateContext()) 
			{
				var records = new List<TEntity>(capacity: entities.Count);
				for(var i = 0; i < entities.Count; i++) 
				{
					records.Add(CreateEntity(entities[i]));
				}
				GetDbSet(context).AddRange(records);

				return context.SaveChanges();
			}
		}

		public virtual async Task<int> AddRangeAsync(IReadOnlyList<TIEntity> entities, CancellationToken cancellationToken = default)
		{
			using(var context = await CreateContexAsync(cancellationToken)
				.ConfigureAwait(continueOnCapturedContext:false))
			{
				var records = new List<TEntity>(capacity: entities.Count);
				for(var i = 0; i < entities.Count; i++)
				{
					cancellationToken.ThrowIfCancellationRequested();

					records.Add(CreateEntity(entities[i]));
				}
				GetDbSet(context).AddRange(records);

				return await context
					.SaveChangesAsync(cancellationToken)
					.ConfigureAwait(continueOnCapturedContext: false);
			}
		}

		public virtual int RemoveByFilter(TFilter filter) 
		{
			using(var context = CreateContext()) 
			{
				var dbSet = GetDbSet(context);
				var query = dbSet.AsQueryable();
				query = ApplyFilter(query, filter);

				var recordCount = query.Count();
				if(recordCount > RemoveLimit)
				{
					var windowCount = (int)Math.Round(recordCount * 1.0d / RemoveLimit);
					var removedCount = 0;
					for(var i = 0; i < windowCount; i++)
					{
						removedCount += BatchRemove(filter);
					}
				}
				else 
				{
					var records = query.ToArray();
					dbSet.RemoveRange(records);
				}

				return context.SaveChanges();
			}
		}

		public virtual async Task<int> RemoveByFilterAsync(TFilter filter, CancellationToken cancellationToken = default)
		{
			using(var context = await CreateContexAsync(cancellationToken)
				.ConfigureAwait(continueOnCapturedContext: false))
			{
				var dbSet = GetDbSet(context);
				var query = dbSet.AsQueryable();
				query = ApplyFilter(query, filter);

				var recordCount = await query.CountAsync(cancellationToken)
					.ConfigureAwait(continueOnCapturedContext: false);
				if(recordCount > RemoveLimit)
				{
					var windowCount = (int)Math.Round(recordCount * 1.0d / RemoveLimit);
					var removedCount = 0;
					for(var i = 0; i < windowCount; i++)
					{
						removedCount += await BatchRemoveAsync(filter, cancellationToken)
							.ConfigureAwait(continueOnCapturedContext: false);
					}
				}
				else
				{
					var records = await query.ToArrayAsync(cancellationToken)
						.ConfigureAwait(continueOnCapturedContext: false);
					dbSet.RemoveRange(records);
				}

				return await context
					.SaveChangesAsync(cancellationToken)
					.ConfigureAwait(continueOnCapturedContext: false);
			}
		}

		private int BatchRemove(TFilter filter) 
		{
			using(var context = CreateContext()) 
			{
				var dbSet = GetDbSet(context);
				var query = dbSet.AsQueryable();
				query = ApplyFilter(query, filter);

				var records = query
					.Take(() => RemoveLimit)
					.ToArray();
				dbSet.RemoveRange(records);

				return context.SaveChanges();
			}
		}

		private async Task<int> BatchRemoveAsync(TFilter filter, CancellationToken cancellationToken)
		{
			using(var context = await CreateContexAsync(cancellationToken)
				.ConfigureAwait(continueOnCapturedContext: false))
			{
				var dbSet = GetDbSet(context);
				var query = dbSet.AsQueryable();
				query = ApplyFilter(query, filter);

				var records = await query
					.Take(() => RemoveLimit)
					.ToArrayAsync(cancellationToken)
					.ConfigureAwait(continueOnCapturedContext: false);

				dbSet.RemoveRange(records);

				return await context
					.SaveChangesAsync(cancellationToken)
					.ConfigureAwait(continueOnCapturedContext: false);
			}
		}

		private Context CreateContext() => new Context(ConnectionFactory.CreateConnection());

		private Task<Context> CreateContexAsync(CancellationToken cancellationToken) => Task.Run(CreateContext, cancellationToken);
	}
}
