using BS92IB_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS92IB_HFT_2021222.Repository
{
    public interface IWeaponRepository
    {
        void Create(Weapon weapon);
        Weapon Read(int id);
        void Update(Weapon weapon);
        void Delete(int id);
        IQueryable<Weapon> ReadAll();
    }
}
