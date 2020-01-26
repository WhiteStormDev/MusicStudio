using BLInterfaces;
using DBLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using MusicStudioModels;
namespace Services.Controllers
{
	[EnableCors(origins: "*", headers: "*", methods: "*")]
	public class AbonementsController : ApiController
	{
        IAbonementService _service;
        public AbonementsController(IAbonementService service)
        {
            _service = service;
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        // GET api/orders
        public HttpResponseMessage Get(int page = 1, int pageLen = 10, string sortBy = "", string sort = "")
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   _service.FindPaged(page, pageLen, sortBy, sort),
                    Configuration.Formatters.JsonFormatter);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
#if DEBUG
                    ex.ToString()
#else
                    "Server error occured"
#endif
                    );
            }
        }

        // GET api/orders/5
        public string Get(int id)
        {
            return "order";
        }

        // POST api/orders
        public void Post([FromBody]string order)
        {
        }

        // PUT api/orders/5
        public void Put(int id, [FromBody]string order)
        {
        }

        // DELETE api/orders/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (var context = new musicstudiodbContext())
                {
                    var a = (from c in context.Abonements where c.Id == id select c).FirstOrDefault();
                    if (a == null)
                    {
                        
                        return Request.CreateResponse(HttpStatusCode.NotFound);
                    }
                    context.Abonements.Remove(a);
                    context.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (DbUpdateException dbex)
            {
                return Request.CreateResponse(HttpStatusCode.Conflict,
#if DEBUG
                    dbex.ToString()
#else
                    "There are some entities in the database that should be deleted first"
#endif
                    );
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
#if DEBUG
                    ex.ToString()
#else
                    "Server error occured"
#endif
                    );
            }
        }
    }
}
