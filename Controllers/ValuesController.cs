using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NEventStore;

namespace DotNetCoreHelloWorld.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private static int _counter;
        private static int _lastReadMessage;

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var result = new List<string>();
            using (var stream = Program.EventStore.OpenStream(Program.BucketId, Program.StreamId, _lastReadMessage, int.MaxValue))
            {
                foreach (var message in stream.CommittedEvents)
                {
                    var body = ((ValuesEvent)message.Body);
                    result.Add($"{body.Value} message: {body.UserInput}");
                    _lastReadMessage++;
                }
            }
            return result;
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
            using (var stream = Program.EventStore.OpenStream(Program.BucketId, Program.StreamId, 0, int.MaxValue))
            {
                stream.Add(new EventMessage { Body = new ValuesEvent(_counter++, value) });
                stream.CommitChanges(Guid.NewGuid());
            }
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
