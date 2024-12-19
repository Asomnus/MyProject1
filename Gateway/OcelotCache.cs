//using Glimpse.AspNet.Model;
//using Ocelot.Cache;
//using System.Collections.Concurrent;

//namespace Gateway
//{
//    /// <summary>
//    /// 自定义ocelot缓存
//    /// </summary>
//    public class OcelotCache: IOcelotCache<CachedResponse>
//    {
//        private static ConcurrentDictionary<string, CacheModel> DicCache = new ConcurrentDictionary<string, CacheModel>();

//        public void Add(string key, CachedResponse value, TimeSpan ttl, string region)
//        {
//            throw new NotImplementedException();
//        }

//        public CachedResponse Get(string key, string region)
//        {
//            throw new NotImplementedException();
//        }

//        public void ClearRegion(string region)
//        {
//            throw new NotImplementedException();
//        }

//        public void AddAndDelete(string key, CachedResponse value, TimeSpan ttl, string region)
//        {
//            throw new NotImplementedException();
//        }

//    }
//}
