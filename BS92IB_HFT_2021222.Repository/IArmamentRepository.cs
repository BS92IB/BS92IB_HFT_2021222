using BS92IB_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS92IB_HFT_2021222.Repository
{
    public interface IArmamentRepository
    {
        void Create(Armament armament);
        Armament Read(int id);
        void Update(Armament armament);
        void Delete(int id);
        IQueryable<Armament> ReadAll();
    }
}
