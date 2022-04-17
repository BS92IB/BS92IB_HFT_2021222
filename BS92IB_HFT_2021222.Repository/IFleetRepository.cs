using BS92IB_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS92IB_HFT_2021222.Repository
{
    public interface IFleetRepository
    {
        void Create(Fleet fleet);
        Fleet Read(int id);
        void Update(Fleet fleet);
        void Delete(int id);
        IQueryable<Fleet> ReadAll();
    }
}
