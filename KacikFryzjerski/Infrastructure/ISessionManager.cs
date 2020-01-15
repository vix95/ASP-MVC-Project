using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KacikFryzjerski.Infrastructure
{
    public interface ISessionManager
    {
        T GetT<T>(string key);
        void Set<T>(string name, T value);
        void Abandon(); //delete element
        T TryGet<T>(string key);
    }
}
