using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    //Interface of Distributed and In-memory cache 
    public interface ICacheExample
    {
        //Distributed Cache 
        public void DistSetString(string key, string value);
        public void DistSetList(string key);
        public string DistGetValue(string key);

        //In Memory Cache
        public void InMemSetString(string key, string value);
        public void InMemSetObject(string key, object value);
        public T InMemGetValue<T>(string key);
    }
}
