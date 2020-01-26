using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicStudioModels
{
	public class ClientModel
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string SecondName { get; set; }
		public string Surname { get; set; }
		public string PhoneNumber { get; set; }
		public int RemainingAbonementsCount { get; set; }
	}
}