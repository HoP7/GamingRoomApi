using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GaminRoom.Domain;
using GaminRoom.Domain.Helpers;
using GaminRoom.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace GamingRoom.Controllers
{
    [AuthorizationAttribute]
    [Route("api/[controller]")]
    public class CodeController : BaseController
    {
        public CodeController(GamingRoomContext db) : base(db)
        {
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("generateCode")]
        public string GenerateCode()
        {
            var generatedCode = RandomString.GenerateString(7);
            var user = HttpContext.GetUser();
            var code = new Code
            {
                UserId = user.Id,
                GeneratedCode = generatedCode,
                DateExpired = DateTime.Now.AddDays(1)
            };
            _db.Codes.Add(code);
            _db.SaveChanges();
            return generatedCode;
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
