using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS92IB_HFT_2021222.Client
{
    public interface IRestService
    {
        void Delete(int id, string endpoint);
        T Get<T>(int id, string endpoint);
        List<T> Get<T>(string endpoint);
        T GetSingle<T>(string endpoint);
        void Post<T>(T item, string endpoint);
        void Put<T>(T item, string endpoint);
    }
}
