namespace RedemptionTest.Services
{
    public class GraphQLCollectionSegment<T>
    {
        public int? TotalCount { get; set; }
        public List<T>? Items { get; set; }
    }

    public class PosType
    {
        public GraphQLCollectionSegment<PosInfoItem> PosInfo { get; set; }
        public GraphQLCollectionSegment<PosItem> Pos { get; set; }
    }

    public class PosInfoItem
    {
        public int id { get; set; }
        public int posId { get; set; }
        public string workKey { get; set; }
    }

    public class PosItem
    {
        public int id { get; set; }
        public int programId { get; set; }
        public int merchantId { get; set; }
        public int? shopId { get; set; }
        public string terminalIdentityCode { get; set; }
    }

    public class MerchantProgramType
    {
        public GraphQLCollectionSegment<MerchantInfo> merchants { get; set; }
        public GraphQLCollectionSegment<ProgramInfo> programs { get; set; }
        public GraphQLCollectionSegment<ShopInfo> shops { get; set; }
    }

    public class MerchantInfo
    {
        public int merchantId { get; set; }
        public int programId { get; set; }
        public string securityKey { get; set; }
        public string identityCode { get; set; }
    }

    public class ProgramInfo
    {
        public int id { get; set; }
        public string identityCode { get; set; }
    }

    public class ShopInfo
    {
        public int merchantId { get; set; }
        public int shopId { get; set; }
        public string identityCode { get; set; }
        public string securityKey { get; set; }
    }

    enum EnumRedeemAction
    {
        TestPOS = 301,
        VerifyVoucher = 104,
        Capture = 101,
        ReverseCapture = 201,
        Authorize = 102,
        CompleteAuthorization = 103,
        ReverseAuthorization = 202,
        ReverseAuthorizedCapture = 203
    }
}
