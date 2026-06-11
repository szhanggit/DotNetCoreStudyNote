using Services.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Proto.Product;

namespace Services.Core
{
    public class UpdateProductAcceptanceLoopService : IUpdateProductAcceptanceLoopService
    {
        private IDbConnection _dbConnection;

        public async Task<ProtoBaseResponse> UpdateProductAcceptanceLoop(UpdateProductAcceptanceLoopRequest request)
        {
            try
            {                   
                int _result = 1;

                if (_result == 0)
                {
                    return new ProtoBaseResponse
                    {
                        Success = false,
                        Message = "Nothing will be changed"
                    };
                }
                else if (_result != 1)
                {
                    return new ProtoBaseResponse
                    {
                        Success = false,
                        Message = "Fail"
                    };
                }
                else
                {
                    return new ProtoBaseResponse
                    {
                        Success = true,
                        Message = "Success"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ProtoBaseResponse() { Success = false, Message = ex.Message };
            }
        }
    }
}
