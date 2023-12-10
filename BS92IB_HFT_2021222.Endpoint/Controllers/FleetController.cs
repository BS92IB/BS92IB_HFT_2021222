using BS92IB_HFT_2021222.Endpoint.Services;
using BS92IB_HFT_2021222.Logic;
using BS92IB_HFT_2021222.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BS92IB_HFT_2021222.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FleetController : ControllerBase
    {
        private IFleetLogic fleetLogic;
        IHubContext<SignalRHub> hub;

        public FleetController(IFleetLogic fleetLogic, IHubContext<SignalRHub> hub)
        {
            this.fleetLogic = fleetLogic;
            this.hub = hub;
        }

        // GET: /fleet
        [HttpGet]
        public IEnumerable<Fleet> Get()
        {
            return fleetLogic.ReadAll();
        }

        // GET /fleet/5
        [HttpGet("{id}")]
        public Fleet Get(int id)
        {
            return fleetLogic.Read(id);
        }

        // POST /fleet
        [HttpPost]
        public void Post([FromBody] Fleet value)
        {
            fleetLogic.Create(value);
            this.hub.Clients.All.SendAsync("FleetCreated", value);
        }

        // PUT /fleet
        [HttpPut]
        public void Put([FromBody] Fleet value)
        {
            fleetLogic.Update(value);
            this.hub.Clients.All.SendAsync("FleetUpdated", value);
        }

        // DELETE /fleet/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var fleetToDelete = this.fleetLogic.Read(id);
            fleetLogic.Delete(id);
            this.hub.Clients.All.SendAsync("FleetDeleted", fleetToDelete);
        }
    }
}
