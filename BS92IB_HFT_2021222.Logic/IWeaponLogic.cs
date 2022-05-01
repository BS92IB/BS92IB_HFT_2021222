using BS92IB_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS92IB_HFT_2021222.Logic
{
    public interface IWeaponLogic
    {
        void Create(Weapon weapon);
        Weapon Read(int id);
        void Update(Weapon weapon);
        void Delete(int id);
        IEnumerable<Weapon> ReadAll();
    }
}
