using MusicStudioModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLInterfaces
{
	public interface IAbonementService
	{
		PagedResult<AbonementModel> FindPaged(int page = 1, int pageLen = 10, string sortBy = "", string sort = "");
	}
}
