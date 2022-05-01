using BS92IB_HFT_2021222.Models;
using BS92IB_HFT_2021222.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS92IB_HFT_2021222.Logic
{
    public class FleetLogic : IFleetLogic
    {

        IFleetRepository fleetRepo;

        public FleetLogic(IFleetRepository fleetRepo)
        {
            this.fleetRepo = fleetRepo;
        }

        public void Create(Fleet fleet)
        {
            if (string.IsNullOrEmpty(fleet.Name))
            {
                throw new ArgumentException("Name cannot be Null.");
            }
            fleetRepo.Create(fleet);
        }

        public void Delete(int id)
        {
            fleetRepo.Delete(id);
        }

        public Fleet FastestFleet()
        {
            var ff = fleetRepo.ReadAll()
                .Select(f => new { Fleet = f, FleetSpeed = f.Ships.Min(s => s.MaxSpeedKnots) })
                .OrderByDescending(x => x.FleetSpeed)
                .FirstOrDefault()
                .Fleet;
            return ff;
        }

        public IEnumerable<Fleet> FleetsWithoutCarrier()
        {
            var f = fleetRepo.ReadAll()
                .Where(f => !f.Ships.Any(s => s.HullType == "Carrier"));
            return f;
        }

        public Fleet MostArmedFleet()
        {
            var query = from fleet in fleetRepo.ReadAll()
                        from ship in fleet.Ships
                        from armament in ship.Armaments
                        group armament by fleet.Id into g  // EF can't translate to SQL with fleet being here                    
                        select new { FleetId = g.Key, Sum = g.Sum(x => x.Quantity) } into s
                        orderby s.Sum descending
                        select s.FleetId;
            var fleetId = query.FirstOrDefault();
            return fleetRepo.Read(fleetId);
        }

        public Fleet Read(int id)
        {
            return fleetRepo.Read(id);
        }

        public IEnumerable<Fleet> ReadAll()
        {
            return fleetRepo.ReadAll();
        }

        public int TotalAaGuns(Fleet fleet)
        {
            var totalAaGuns = fleetRepo.ReadAll()
                .Where(f => f.Id == fleet.Id)
                .SelectMany(f => f.Ships)
                .SelectMany(s => s.Armaments)
                .Where(a => a.Weapon.WeaponType == "AA gun")
                .Sum(a => a.Quantity);
            return totalAaGuns;
        }

        public double TotalDisplacement(Fleet id)
        {
            var totalDisplacement = fleetRepo.ReadAll()
                .Where(f => f.Id == id.Id)
                .SelectMany(f => f.Ships)
                .Sum(s => s.Displacement);
            return totalDisplacement;
        }

        public void Update(Fleet fleet)
        {
            fleetRepo.Update(fleet);
        }
    }
}
