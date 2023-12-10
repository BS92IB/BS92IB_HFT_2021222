using BS92IB_HFT_2021222.Endpoint.Services;
using BS92IB_HFT_2021222.Logic;
using BS92IB_HFT_2021222.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
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
        IHubContext<SignalRHub> hub;

        public ArmamentController(IArmamentLogic armamentLogic, IHubContext<SignalRHub> hub)
        {
            this.armamentLogic = armamentLogic;
            this.hub = hub;
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
            this.hub.Clients.All.SendAsync("ArmamentCreated", value);
        }

        // PUT /armament
        [HttpPut]
        public void Put([FromBody] Armament value)
        {
            armamentLogic.Update(value);
            this.hub.Clients.All.SendAsync("ArmamentUpdated", value);
        }

        // DELETE /armament/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var armamentToDelete = this.armamentLogic.Read(id);
            armamentLogic.Delete(id);
            this.hub.Clients.All.SendAsync("ArmamentDeleted", armamentToDelete);
        }
    }
}
