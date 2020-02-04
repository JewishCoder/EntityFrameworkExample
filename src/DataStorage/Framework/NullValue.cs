using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorage.Framework
{
	public sealed class NullValue<T> where T : struct
	{
		public T? Value { get; }
	}
}
