using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebApiCoreDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public IHostingEnvironment HostingEnvironment { get; }

        public ValuesController(IHostingEnvironment hostingEnvironment)
        {
            HostingEnvironment = hostingEnvironment;
        }
        // GET api/values
        //[HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //    var filePath = System.IO.Path.Combine( HostingEnvironment.ContentRootPath,"Files","data.txt");
        //    var text = System.IO.File.ReadAllText(filePath);
        //   return Content(text);
        //}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetAsync()
        {
            var filePath = System.IO.Path.Combine(HostingEnvironment.ContentRootPath, "Files", "data.txt");
            var text = await System.IO.File.ReadAllTextAsync(filePath);
            return Content(text);
        }



        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
