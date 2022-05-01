using BS92IB_HFT_2021222.Models;
using BS92IB_HFT_2021222.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS92IB_HFT_2021222.Logic
{
    public class ArmamentLogic : IArmamentLogic
    {
        IArmamentRepository armamentRepo;
        public ArmamentLogic(IArmamentRepository armamentRepo)
        {
            this.armamentRepo = armamentRepo;
        }
        public void Create(Armament armament)
        {
            if (string.IsNullOrEmpty(armament.Name))
            {
                throw new ArgumentException("Name cannot be Null.");
            }
            if (armament.Quantity <= 0)
            {
                throw new ArgumentException("Quantity cannot be <= 0");
            }
            armamentRepo.Create(armament);
        }

        public void Delete(int id)
        {
            armamentRepo.Delete(id);
        }

        public Armament Read(int id)
        {
            return armamentRepo.Read(id);
        }

        public IEnumerable<Armament> ReadAll()
        {
            return armamentRepo.ReadAll();
        }

        public void Update(Armament armament)
        {
            armamentRepo.Update(armament);
        }
    }
}
