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
    public class WeaponController : ControllerBase
    {
        private IWeaponLogic weaponLogic;
        IHubContext<SignalRHub> hub;

        public WeaponController(IWeaponLogic weaponLogic, IHubContext<SignalRHub> hub)
        {
            this.weaponLogic = weaponLogic;
            this.hub = hub;
        }

        // GET: /weapon
        [HttpGet]
        public IEnumerable<Weapon> Get()
        {
            return weaponLogic.ReadAll();
        }

        // GET /weapon/5
        [HttpGet("{id}")]
        public Weapon Get(int id)
        {
            return weaponLogic.Read(id);
        }

        // POST /weapon
        [HttpPost]
        public void Post([FromBody] Weapon value)
        {
            weaponLogic.Create(value);
            this.hub.Clients.All.SendAsync("WeaponCreated", value);
        }

        // PUT /weapon
        [HttpPut]
        public void Put([FromBody] Weapon value)
        {
            weaponLogic.Update(value);
            this.hub.Clients.All.SendAsync("WeaponUpdated", value);
        }

        // DELETE /weapon/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var weaponToDelete = this.weaponLogic.Read(id);
            weaponLogic.Delete(id);
            this.hub.Clients.All.SendAsync("WeaponDeleted", weaponToDelete);
        }
    }
}
