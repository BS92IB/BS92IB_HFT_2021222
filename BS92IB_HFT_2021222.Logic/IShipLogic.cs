using BS92IB_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS92IB_HFT_2021222.Logic
{
    public interface IShipLogic
    {
        void Create(Ship ship);
        Ship Read(int id);
        void Update(Ship ship);
        void Delete(int id);
        IEnumerable<Ship> ReadAll();
    }
}
