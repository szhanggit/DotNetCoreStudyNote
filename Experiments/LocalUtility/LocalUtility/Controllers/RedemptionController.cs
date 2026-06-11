using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RedemptionTest.Services;
using Tx2.Adora.Utils.Encryption;

namespace RedemptionTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedemptionController : ControllerBase
    {
        public const string VOUCHER_NUMBER = "voucherNumber";
        public const string TRAN_AMOUNT = "tranAmount";
        public const string TRAN_CODE_REF = "tranCodeRef";
        public const string TERMINAL_SSN = "terminalSsn";
        public const string TERMINAL_DATETIME = "terminalDateTime";
        public const string PIN_CODE = "pinCode";

        public const string ACTION = "Action";

        public const string PROGRAM_IDENTITY_CODE = "ProgramIdentityCode";
        public const string MERCHANT_IDENTITY_CODE = "MerchantIdentityCode";
        public const string SHOP_IDENTITY_CODE = "shopIdentityCode";
        public const string TERMINAL_IDENTITY_CODE = "terminalIdentityCode";
        public const string CHANNEL = "Channel";

        private readonly ICacheService _cacheService;

        public RedemptionController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        [HttpGet]
        [Route("GetWorkKey")]
        public string GetWorkKey([FromQuery] string MerchantCode, string ProgramCode)
        {
            return GetWorkKeyFromCache(MerchantCode, ProgramCode);
        }

        [HttpGet]
        [Route("GetChecksum")]
        public string GetChecksum([FromQuery] string ChecksumString)
        {
            return MD5Helper.GetHash(ChecksumString);
        }

        [HttpGet]
        [Route("GetEncryptText")]
        public string GetEncryptText([FromQuery] string PlainText, string WorkKey)
        {
            return TDesHelper.Encrypt((string)PlainText, WorkKey); ;
        }

        [HttpGet]
        [Route("GetDecryptText")]
        public string GetDecryptText([FromQuery] string PlainText, string WorkKey)
        {
            return TDesHelper.Decrypt((string)PlainText, WorkKey); ;
        }

        [HttpPost]
        public string Post([FromBody] string value)
        {
            JObject jsonObject = JObject.Parse(value);

            Request.Headers.TryGetValue(PROGRAM_IDENTITY_CODE, out var programIdentityCode);
            Request.Headers.TryGetValue(SHOP_IDENTITY_CODE, out var shopIdentityCode);
            Request.Headers.TryGetValue(TERMINAL_IDENTITY_CODE, out var terminalIdentityCode);
            Request.Headers.TryGetValue(MERCHANT_IDENTITY_CODE, out var merchantIdentityCode);
            Request.Headers.TryGetValue(CHANNEL, out var channel);

            Request.Headers.TryGetValue(ACTION, out var action);

            Enum.TryParse(typeof(EnumRedeemAction), action, true, out var redeemAction);

            jsonObject.TryGetValue(VOUCHER_NUMBER, out var voucherNumber);
            jsonObject.TryGetValue(TRAN_AMOUNT, out var tranAmount);
            jsonObject.TryGetValue(TRAN_CODE_REF, out var tranCodeRef);
            jsonObject.TryGetValue(TERMINAL_SSN, out var terminalSsn);
            jsonObject.TryGetValue(TERMINAL_DATETIME, out var terminalDateTime);
            jsonObject.TryGetValue(PIN_CODE, out var pinCode);

            voucherNumber = voucherNumber ?? string.Empty;
            pinCode = pinCode ?? string.Empty;
            terminalSsn = terminalSsn ?? string.Empty;
            tranCodeRef = tranCodeRef ?? string.Empty;
            tranAmount = tranAmount ?? string.Empty;
            terminalDateTime = terminalDateTime ?? string.Empty;

            DateTime.TryParse(terminalDateTime.ToString(), out var TerminalDateTime);

            var workKey = GetWorkKeyFromCache(merchantIdentityCode, programIdentityCode);
            string checksum = String.Empty;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"The workkey for merchant {merchantIdentityCode} is {workKey}");

            if (voucherNumber != null && redeemAction != null)
            {
                var encryptedVoucherNo = TDesHelper.Encrypt((string)voucherNumber.ToString(), workKey);
                var encryptedPinCode = TDesHelper.Encrypt((string)pinCode.ToString(), workKey);

                jsonObject[VOUCHER_NUMBER] = JToken.FromObject(encryptedVoucherNo);
                jsonObject[PIN_CODE] = JToken.FromObject(encryptedPinCode);

                string checksumString = $"{voucherNumber}={channel}=" +
                $"{merchantIdentityCode}={programIdentityCode}=" +
                $"{shopIdentityCode}={terminalIdentityCode}=" +
                $"{terminalSsn}={tranAmount}={tranCodeRef}=" +
                $"{TerminalDateTime.ToString("yyyyMMddHHmmss")}={(int)redeemAction}={workKey}";

                checksum = MD5Helper.GetHash(checksumString);

                Console.WriteLine($"The checksum is {checksum}");
            }

            Console.ForegroundColor = ConsoleColor.White;

            dynamic result = new JObject();
            result.payload = jsonObject.ToString();
            result.checksum = checksum;
            result.workkey = workKey;

            return result.ToString();
        }

        private string GetWorkKeyFromCache(string MerchantCode, string ProgramCode)
        {
            var workKeyItem = _cacheService.GetCachedList()
                .Where(x => x.merchantCode == MerchantCode && x.programCode == ProgramCode)
                .First();

            return workKeyItem == null ? String.Empty : workKeyItem.workKey;
        }
    }
}
