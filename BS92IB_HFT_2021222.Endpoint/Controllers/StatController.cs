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
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        IFleetLogic fl;

        public StatController(IFleetLogic fl)
        {
            this.fl = fl;
        }

        [HttpGet]
        public Fleet FastestFleet()
        {
            return fl.FastestFleet();
        }
        [HttpGet("{id}")]
        public double TotalDisplacement(int id)
        {
            Fleet fleet = fl.Read(id);
            return fl.TotalDisplacement(fleet);
        }
        [HttpGet("{id}")]
        public int TotalAaGuns(int id)
        {
            Fleet fleet = fl.Read(id);
            return fl.TotalAaGuns(fleet);
        }
        [HttpGet]
        public Fleet MostArmedFleet()
        {
            return fl.MostArmedFleet();
        }
        [HttpGet]
        public IEnumerable<Fleet> FleetsWithoutCarrier()
        {
            return fl.FleetsWithoutCarrier();
        }
    }
}
