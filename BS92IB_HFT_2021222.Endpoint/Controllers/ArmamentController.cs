using BS92IB_HFT_2021222.Logic;
using BS92IB_HFT_2021222.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BS92IB_HFT_2021222.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ArmamentController : ControllerBase
    {
        private IArmamentLogic armamentLogic;

        public ArmamentController(IArmamentLogic armamentLogic)
        {
            this.armamentLogic = armamentLogic;
        }

        // GET: /armament
        [HttpGet]
        public IEnumerable<Armament> Get()
        {
            return armamentLogic.ReadAll();
        }

        // GET /armament/5
        [HttpGet("{id}")]
        public Armament Get(int id)
        {
            return armamentLogic.Read(id);
        }

        // POST /armament
        [HttpPost]
        public void Post([FromBody] Armament value)
        {
            armamentLogic.Create(value);
        }

        // PUT /armament
        [HttpPut]
        public void Put([FromBody] Armament value)
        {
            armamentLogic.Update(value);
        }

        // DELETE /armament/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            armamentLogic.Delete(id);
        }
    }
}
