namespace RedemptionTest.Services
{
    public interface ICacheService
    {
        List<POSAndMerchant> GetCachedList();
        void AddToCache(List<POSAndMerchant> item);
    }

    public class CacheService: ICacheService
    {
        private readonly CacheList _cacheList;

        public CacheService(CacheList cacheList)
        {
            _cacheList = cacheList;
        }

        public List<POSAndMerchant> GetCachedList()
        {
            return _cacheList.POSAndMerchants;
        }

        public void AddToCache(List<POSAndMerchant> item)
        {
            _cacheList.POSAndMerchants.AddRange(item);
        }
    }

    public class CacheList
    {
        public List<POSAndMerchant> POSAndMerchants { get; set; } = new List<POSAndMerchant>();
    }

    public class POSAndMerchant
    {
        public int? posId { get; set; }
        public string workKey { get; set; }
        public string programCode { get; set; }
        public string terminalIdentityCode { get; set; }
        public string merchantCode { get; set; }
    }
}
