// Disclaimer
// Dieser Quellcode ist als Vorlage oder als Ideengeber gedacht. Er kann frei und ohne 
// Auflagen oder Einschränkungen verwendet oder verändert werden.
// Jedoch wird keine Garantie übernommen, das eine Funktionsfähigkeit mit aktuellen und 
// zukünftigen API-Versionen besteht. Der Autor übernimmt daher keine direkte oder indirekte 
// Verantwortung, wenn dieser Code gar nicht oder nur fehlerhaft ausgeführt wird.
// Für Anregungen und Fragen stehe ich jedoch gerne zur Verfügung.
// Thorsten Kansy, www.dotnetconsulting.eu
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using dotnetconsulting.EfWebApi.DAL;

namespace dotnetconsulting.EfWebApi.Controllers
{

    [Route("api/[controller]")]
    public class VLogsController : Controller
    {
        TravelLogContext _TravelLogContext;

        public VLogsController(TravelLogContext TravelLogContext)
        {
            _TravelLogContext = TravelLogContext;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<TravelVlog> Get()
        {
            return _TravelLogContext.TravelLogs.Take(10);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
