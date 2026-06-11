using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;

namespace RedemptionTest.Services
{
    public class HostedService : IHostedService
    {
        private readonly ICacheService _cacheService;
        private readonly IConfiguration _configuration;

        public const string GRAPHQL_HEADER_TENANT_ID = "TenantId";
        public const string REDEMPTION_GRAPHQL_URL = "RedemptionGraphQLURL";
        public const string MERCHANT_GRAPHQL_URL = "MerchantGraphQLURL";
        public string TenantId { get; set; }
        public string RedemptionGraphQLURL { get; set; }
        public string MerchantGraphQLURL { get; set; }

        public HostedService(ICacheService cacheService, IConfiguration configuration)
        {
            _cacheService = cacheService;
            _configuration = configuration;

            TenantId = _configuration[GRAPHQL_HEADER_TENANT_ID];
            RedemptionGraphQLURL = _configuration[REDEMPTION_GRAPHQL_URL];
            MerchantGraphQLURL = _configuration[MERCHANT_GRAPHQL_URL];
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            //Startup logic here
            PosType pos = await GetPOSInfo();

            var shops = pos.Pos.Items!
                .Where(x => x.shopId != null)
                .Select(x => x.shopId).ToList();

            MerchantProgramType merchantProgram = await GetMerchantInfo(shops);

            var posAndMerchant = (from i in pos.PosInfo.Items!
                                  join p in pos.Pos.Items! on i.posId equals p.id
                                  join m in merchantProgram.merchants.Items! on p.merchantId equals m.merchantId
                                  join program in merchantProgram.programs.Items! on p.programId equals program.id
                                  select new POSAndMerchant
                                  {
                                      posId = i.posId,
                                      workKey = i.workKey,
                                      merchantCode = m.identityCode,
                                      programCode = program.identityCode,
                                      terminalIdentityCode = p.terminalIdentityCode,
                                  }).ToList();

            _cacheService.AddToCache(posAndMerchant);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{pos.Pos.TotalCount} pos are loaded") ;
            Console.WriteLine($"{pos.PosInfo.TotalCount} posInfos are Loaded");
            Console.WriteLine($"{merchantProgram.merchants.TotalCount} merchants are loaded");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task<PosType> GetPOSInfo()
        {
            var query = new GraphQLRequest
            {
                Query = @"
    query {
        posInfo {
            totalCount
            items {
                id
                posId
                workKey
            }
        }
        pos {
            totalCount
            items {
                id
                programId
                merchantId
                shopId
                terminalIdentityCode
            }
        }
    }"
            };

            GraphQLHttpClient Client = new GraphQLHttpClient(RedemptionGraphQLURL, new NewtonsoftJsonSerializer());
            Client.HttpClient.DefaultRequestHeaders.Add(GRAPHQL_HEADER_TENANT_ID, TenantId);

            var response = await Client.SendQueryAsync<PosType>(query, cancellationToken: default);

            return response.Data ?? new PosType();
        }

        public async Task<MerchantProgramType> GetMerchantInfo(IEnumerable<int?> shopIds)
        {
            var query = new GraphQLRequest
            {
                Query = @"
    query($shopIds:[Int!]) {
        merchants {
            totalCount
            items {
                merchantId
                programId
                securityKey
                identityCode
            }
        }
        programs {
            totalCount
            items {
                identityCode
                id
            }
        }
        shops(where: { shopId: { in: $shopIds } }) {
            totalCount
            items {
                identityCode
                shopId
                merchantId
                securityKey
            }
        }
    }",
                Variables = new { shopIds = shopIds }
            };

            GraphQLHttpClient Client = new GraphQLHttpClient(MerchantGraphQLURL, new NewtonsoftJsonSerializer());
            Client.HttpClient.DefaultRequestHeaders.Add(GRAPHQL_HEADER_TENANT_ID, TenantId);

            var response = await Client.SendQueryAsync<MerchantProgramType>(query, cancellationToken: default);
            return response.Data ?? new MerchantProgramType();
        }
    }
}
