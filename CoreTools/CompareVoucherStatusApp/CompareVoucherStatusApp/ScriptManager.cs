using System;
using System.Collections.Generic;
using System.Text;

namespace CompareVoucherStatusApp
{
    public static class ScriptManager
    {
        public static string GetExtendVoucherSyncInfo()
        {
            string sql = string.Empty;
            sql = @"
                Declare @SessionId varchar(100) = newId();
                Declare @Info Table(Id int null, VoucherNumber varchar(50), ProgramCode varchar(20)); 
                update top(@Step) ExtendVoucher 
                set SessionId = @SessionId 
                OUTPUT INSERTED.Id, INSERTED.VoucherNumber, INSERTED.ProgramCode into @Info(Id, VoucherNumber, ProgramCode) 
                where SessionId is null
                select Id, VoucherNumber, ProgramCode from @Info
            ";
            return sql;
        }

        public static string GetExtendVoucherSyncInfo2()
        {
            string sql = string.Empty;
            sql = @"
                Declare @SessionId varchar(100) = newId();
                Declare @Info Table(Id int null, VoucherNumber varchar(50), ProgramCode varchar(20), EndClient varchar(100)); 
                update top(@Step) ExtendVoucher 
                set SessionId = @SessionId 
                OUTPUT INSERTED.Id, INSERTED.VoucherNumber, INSERTED.ProgramCode, INSERTED.EndClient into @Info(Id, VoucherNumber, ProgramCode, EndClient) 
                where SessionId is null
                select Id, VoucherNumber, ProgramCode, EndClient from @Info
            ";
            return sql;
        }
    }
}
