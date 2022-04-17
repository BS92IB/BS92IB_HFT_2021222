using BS92IB_HFT_2021222.Data;
using BS92IB_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS92IB_HFT_2021222.Repository
{
    public class FleetRepository : IFleetRepository
    {
        private readonly NavyDbContext db;
        public FleetRepository(NavyDbContext db)
        {
            this.db = db;
        }
        public void Create(Fleet fleet)
        {
            db.Fleets.Add(fleet);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var fleetsToDelete = Read(id);
            db.Fleets.Remove(fleetsToDelete);
            db.SaveChanges();
        }

        public Fleet Read(int id)
        {
            return db.Fleets
                .FirstOrDefault(t => t.Id == id);
        }

        public IQueryable<Fleet> ReadAll()
        {
            return db.Fleets;
        }

        public void Update(Fleet fleet)
        {
            var oldFleet = Read(fleet.Id);
            oldFleet.Name = fleet.Name;
            db.SaveChanges();
        }
    }
}
