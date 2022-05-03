using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS92IB_HFT_2021222.Client
{
    /// <summary>
    /// Fake REST service, for testing the menu without needing the server.
    /// Doesn't do anything, only returns empty results.
    /// </summary>
    class OfflineRestService : IRestService
    {
        public void Delete(int id, string endpoint)
        {

        }

        public T Get<T>(int id, string endpoint)
        {
            return default(T);
        }

        public List<T> Get<T>(string endpoint)
        {
            return new List<T>();
        }

        public T GetSingle<T>(string endpoint)
        {
            return default(T);
        }

        public void Post<T>(T item, string endpoint)
        {

        }

        public void Put<T>(T item, string endpoint)
        {

        }
    }
}
