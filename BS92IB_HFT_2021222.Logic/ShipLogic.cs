using BS92IB_HFT_2021222.Models;
using BS92IB_HFT_2021222.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS92IB_HFT_2021222.Logic
{
    public class ShipLogic : IShipLogic
    {
        IShipRepository shipRepo;
        public ShipLogic(IShipRepository shipRepo)
        {
            this.shipRepo = shipRepo;
        }
        public void Create(Ship ship)
        {
            if (string.IsNullOrEmpty(ship.Name))
            {
                throw new ArgumentException("Name cannot be Null.");
            }
            if (string.IsNullOrEmpty(ship.Class))
            {
                throw new ArgumentException("Class cannot be Null.");
            }
            if (string.IsNullOrEmpty(ship.HullType))
            {
                throw new ArgumentException("HullType cannot be Null.");
            }
            if ((ship.Displacement <= 0) || (ship.Length <= 0) || (ship.Beam <= 0) || (ship.Draft <= 0))
            {
                throw new ArgumentException("Displacement and Sizes cannot be <= 0");
            }
            if (ship.MaxSpeedKnots < 0)
            {
                throw new ArgumentException("MaxSpeed cannot be < 0");
            }
            shipRepo.Create(ship);
        }

        public void Delete(int id)
        {
            shipRepo.Delete(id);
        }

        public Ship Read(int id)
        {
            return shipRepo.Read(id);
        }

        public IEnumerable<Ship> ReadAll()
        {
            return shipRepo.ReadAll();
        }

        public void Update(Ship ship)
        {
            shipRepo.Update(ship);
        }
    }
}
