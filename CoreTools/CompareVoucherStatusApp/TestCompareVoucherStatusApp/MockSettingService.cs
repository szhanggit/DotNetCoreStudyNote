using CompareVoucherStatusApp;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestCompareVoucherStatusApp
{
    public class MockSettingService : ISetting
    {
        public string MoveConnection
        {
            get
            {                
                return "server=172.22.10.222;database=TW_EV_DBS_MOVE;uid=TW_EV_DBS_MOVE;pwd=PZcl9GNAmsffunXd5lCg8pEAG;";
            }
        }

        public string LocalConnection
        {
            get 
            {
                return "Data Source=WLECNSHA1-10023\\SQLSERVER2016;Initial Catalog=LE;Persist Security Info=True;User ID=sa;Password=zh@ng1981;MultipleActiveResultSets=True";
            }
        }
    }
}
