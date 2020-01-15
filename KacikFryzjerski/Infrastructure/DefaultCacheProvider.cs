using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace KacikFryzjerski.Infrastructure
{
    public class DefaultCacheProvider : ICacheProvider
    {
        private Cache cache { get { return HttpContext.Current.Cache; } }
        public object Get(string key)
        {
            return cache[key];
        }

        public void Set(string key, object data, int cashTime)
        {
            var expirationTime = DateTime.Now + TimeSpan.FromMinutes(cashTime);
            cache.Insert(key, data, null, expirationTime, Cache.NoSlidingExpiration);
        }

        public bool IsSet(string key)
        {
            return (cache[key] != null);
        }

        public void Invalidate(string key)
        {
            cache.Remove(key);
        }
    }
}