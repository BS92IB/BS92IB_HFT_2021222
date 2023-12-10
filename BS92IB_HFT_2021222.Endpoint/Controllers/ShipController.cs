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
    public class ShipController : ControllerBase
    {
        private IShipLogic shipLogic;
        IHubContext<SignalRHub> hub;

        public ShipController(IShipLogic shipLogic, IHubContext<SignalRHub> hub)
        {
            this.shipLogic = shipLogic;
            this.hub = hub;
        }

        // GET: /ship
        [HttpGet]
        public IEnumerable<Ship> Get()
        {
            return shipLogic.ReadAll();
        }

        // GET /ship/5
        [HttpGet("{id}")]
        public Ship Get(int id)
        {
            return shipLogic.Read(id);
        }

        // POST /ship
        [HttpPost]
        public void Post([FromBody] Ship value)
        {
            shipLogic.Create(value);
            this.hub.Clients.All.SendAsync("ShipCreated", value);
        }

        // PUT /ship
        [HttpPut]
        public void Put([FromBody] Ship value)
        {
            shipLogic.Update(value);
            this.hub.Clients.All.SendAsync("ShipUpdated", value);
        }

        // DELETE /ship/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var shipToDelete = this.shipLogic.Read(id);
            shipLogic.Delete(id);
            this.hub.Clients.All.SendAsync("ShipDeleted", shipToDelete);
        }
    }
}
