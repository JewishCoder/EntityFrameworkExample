using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataStorage.Framework
{
	public interface IRepository<TIEntity, TFilter>
		where TIEntity : class
		where TFilter : class
	{
		IReadOnlyList<TIEntity> FetchRecords(TFilter filter = null);

		Task<IReadOnlyList<TIEntity>> FetchRecordsAsync(TFilter filter = null, CancellationToken cancellationToken = default);

		int AddRange(IReadOnlyList<TIEntity> entities);

		Task<int> AddRangeAsync(IReadOnlyList<TIEntity> entities, CancellationToken cancellationToken = default);

		int RemoveByFilter(TFilter filter);

		Task<int> RemoveByFilterAsync(TFilter filter, CancellationToken cancellationToken = default);
	}
}
