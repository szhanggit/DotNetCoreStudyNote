
BEGIN
   IF NOT EXISTS (SELECT * FROM [dbo].[tb_tbi_tenant_basic_information] 
                   WHERE country_code = 'TH')
   BEGIN
       INSERT [dbo].[tb_tbi_tenant_basic_information] ([name], [country_code], [timezone], [time_format], [currency_symbol], [company_tax_type], [logo], [preferred_language], [is_active]) VALUES (N'TH', N'TH', N'SE Asia Standard Time', N'yyyy-MM-dd', N'THB', 1, NULL, N'English', 1)
   END
END

BEGIN
   IF NOT EXISTS (SELECT * FROM [dbo].[tb_tbi_tenant_basic_information] 
                   WHERE country_code = 'IN')
   BEGIN
       INSERT [dbo].[tb_tbi_tenant_basic_information] ([name], [country_code], [timezone], [time_format], [currency_symbol], [company_tax_type], [logo], [preferred_language], [is_active]) VALUES (N'IN', N'IN', N'India Standard Time', N'yyyy-MM-dd', N'IN', 1, NULL, N'English', 1)

   END
END

BEGIN
   IF NOT EXISTS (SELECT * FROM [dbo].[tb_tbi_tenant_basic_information] 
                   WHERE country_code = 'ID')
   BEGIN
       INSERT [dbo].[tb_tbi_tenant_basic_information] ([name], [country_code], [timezone], [time_format], [currency_symbol], [company_tax_type], [logo], [preferred_language], [is_active]) VALUES (N'ID', N'ID', N'SE Asia Standard Time', N'yyyy-MM-dd', N'IDR', 1, NULL, N'English', 1)

   END
END

BEGIN
   IF NOT EXISTS (SELECT * FROM [dbo].[tb_tbi_tenant_basic_information] 
                   WHERE country_code = 'JP')
   BEGIN
       INSERT [dbo].[tb_tbi_tenant_basic_information] ([name], [country_code], [timezone], [time_format], [currency_symbol], [company_tax_type], [logo], [preferred_language], [is_active]) VALUES (N'JP', N'JP', N'Tokyo Standard Time', N'yyyy-MM-dd', N'JPY', 1, NULL, N'English', 1)

   END
END

BEGIN
   IF NOT EXISTS (SELECT * FROM [dbo].[tb_tbi_tenant_basic_information] 
                   WHERE country_code = 'GR')
   BEGIN
       INSERT [dbo].[tb_tbi_tenant_basic_information] ([name], [country_code], [timezone], [time_format], [currency_symbol], [company_tax_type], [logo], [preferred_language], [is_active]) VALUES (N'GR', N'GR', N'China Standard Time', N'yyyy-MM-dd', N'USD', 1, NULL, N'English', 1)

   END
END

BEGIN
   IF NOT EXISTS (SELECT * FROM [dbo].[tb_tbi_tenant_basic_information] 
                   WHERE country_code = 'SG')
   BEGIN
       INSERT [dbo].[tb_tbi_tenant_basic_information] ([name], [country_code], [timezone], [time_format], [currency_symbol], [company_tax_type], [logo], [preferred_language], [is_active]) VALUES (N'SG', N'SG', N'Singapore Standard Time', N'yyyy-MM-dd', N'SGD', 1, NULL, N'English', 1)

   END
END

BEGIN
   IF NOT EXISTS (SELECT * FROM [dbo].[tb_tbi_tenant_basic_information] 
                   WHERE country_code = 'TW')
   BEGIN
       INSERT [dbo].[tb_tbi_tenant_basic_information] ([name], [country_code], [timezone], [time_format], [currency_symbol], [company_tax_type], [logo], [preferred_language], [is_active]) VALUES (N'TW', N'TW', N'Taipei Standard Time', N'yyyy-MM-dd', N'TW', 1, NULL, N'English', 1)

   END
END

BEGIN
   IF NOT EXISTS (SELECT * FROM [dbo].[tb_tbi_tenant_basic_information] 
                   WHERE country_code = 'MA')
   BEGIN
       INSERT [dbo].[tb_tbi_tenant_basic_information] ([name], [country_code], [timezone], [time_format], [currency_symbol], [company_tax_type], [logo], [preferred_language], [is_active]) VALUES (N'MA', N'MA', N'Greenwich Standard Time', N'yyyy-MM-dd', N'MAD', 1, NULL, N'English', 1)

   END
END
