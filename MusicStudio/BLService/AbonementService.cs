using BLInterfaces;
using DBLayer;
using MusicStudioModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLService
{
	public class AbonementService : IAbonementService
	{
        Dictionary<string, Func<Abonement, string>> _sortFuncs;

        public AbonementService()
        {
            _sortFuncs = new Dictionary<string, Func<Abonement, string>>();
            _sortFuncs.Add("ClientName", a => a.Client.Surname + " " + a.Client.Name);
            _sortFuncs.Add("ID", a => a.Id.ToString());
            _sortFuncs.Add("DateNext", a => a.NextDate.ToString());
            _sortFuncs.Add("Car", a => a.DateEnd.ToString());
        }

        public PagedResult<AbonementModel> FindPaged(int page = 1, int pageLen = 10, string sortBy = "", string sort = "")
        {
            using (var context = new musicstudiodbContext())
            {
                IEnumerable<Abonement> query = null;
                switch (sort)
                {
                    case "asc":
                        query = (from a in context.Abonements select a).OrderBy(_sortFuncs[sortBy]);
                        break;
                    case "desc":
                        query = (from a in context.Abonements select a).OrderByDescending(_sortFuncs[sortBy]).Skip((page - 1) * pageLen).Take(pageLen);
                        break;
                    default:
                        query = (from a in context.Abonements select a).OrderBy(a => a.Id).Skip((page - 1) * pageLen).Take(pageLen);
                        break;
                }

                int pc = query.Count() / pageLen + 1;

                var sortedEntities = query.ToArray();

                var data = (from a in sortedEntities
                            select new AbonementModel()
                            {
                                Id = a.Id,
                                DateStart = a.DateStart,
                                DateNext = a.NextDate,
                                DateEnd = a.DateEnd,
                                LessonsCount = a.LessonsCount,
                                TeacherSurname = a.Teacher.Surname,
                                ClientId = a.ClientId,
                                SubjectId = a.Teacher.SubjectId
                            }).ToArray();

                return new PagedResult<AbonementModel>() { Page = data, PageCount = pc };
            }

        }
    }
}
