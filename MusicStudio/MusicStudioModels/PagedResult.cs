using System;
using System.Collections.Generic;
using System.Text;

namespace MusicStudioModels
{
	public class PagedResult<T>
	{
		public int PageCount
		{
			get; set;
		}
		public T[] Page;
	}
}
