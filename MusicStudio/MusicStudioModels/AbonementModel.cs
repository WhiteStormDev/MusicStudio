using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicStudio
{
	public class AbonementModel
	{
		public int Id { get; set; }
		public string DateStartStr { get => DateStart.Date.ToShortDateString(); }
		public DateTime DateStart { get; set; }
		public string DateEndStr { get => DateEnd.Date.ToShortDateString(); }
		public DateTime DateEnd { get; set; }
		//public DateTime DateStart { get => _dateStart.Date; set => _dateStart = value; }
		//public DateTime DateEnd { get => _dateEnd.Date; set => _dateEnd = value; }
		//public DateTime? DateNext
		//{
		//	get
		//	{
		//		if (_dateNext != null)
		//		{
		//			return _dateNext.Value;
		//		}
		//		else
		//		{
		//			return null;
		//		}
		//	}
		//	set => _dateNext = value;
		//}
		public string DateNextStr { get => DateNext == null ? "" : ((DateTime)DateNext).Date.ToShortDateString(); }
		
		public DateTime? DateNext { get; set; }

		public int LessonsCount { get; set; }
		public int ClientId { get; set; }
		public int SubjectId { get; set; }
		public string TeacherSurname { get; set; }
		
		//private DateTime _dateStart;
		//private DateTime _dateEnd;
		//private DateTime? _dateNext;
	}
}