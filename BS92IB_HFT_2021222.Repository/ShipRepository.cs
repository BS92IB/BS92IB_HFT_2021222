using BS92IB_HFT_2021222.Data;
using BS92IB_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS92IB_HFT_2021222.Repository
{
    public class ShipRepository : IShipRepository
    {
        private readonly NavyDbContext db;
        public ShipRepository(NavyDbContext db)
        {
            this.db = db;
        }
        public void Create(Ship ship)
        {
            db.Ships.Add(ship);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var shipToDelete = Read(id);
            db.Ships.Remove(shipToDelete);
            db.SaveChanges();
        }

        public Ship Read(int id)
        {
            return db.Ships
                .FirstOrDefault(t => t.Id == id);
        }

        public IQueryable<Ship> ReadAll()
        {
            return db.Ships;
        }

        public void Update(Ship ship)
        {
            var oldShip = Read(ship.Id);
            oldShip.Name = ship.Name;
            oldShip.Class = ship.Class;
            oldShip.HullType = ship.HullType;
            oldShip.Displacement = ship.Displacement;
            oldShip.Length = ship.Length;
            oldShip.Beam = ship.Beam;
            oldShip.Draft = ship.Draft;
            oldShip.MaxSpeedKnots = ship.MaxSpeedKnots;
            oldShip.FleetId = ship.FleetId;
            db.SaveChanges();
        }
    }
}
