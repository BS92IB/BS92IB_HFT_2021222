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
    public class WeaponController : ControllerBase
    {
        private IWeaponLogic weaponLogic;

        public WeaponController(IWeaponLogic weaponLogic)
        {
            this.weaponLogic = weaponLogic;
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
        }

        // PUT /weapon
        [HttpPut]
        public void Put([FromBody] Weapon value)
        {
            weaponLogic.Update(value);
        }

        // DELETE /weapon/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            weaponLogic.Delete(id);
        }
    }
}
