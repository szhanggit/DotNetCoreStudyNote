
-- Insert Countries

BEGIN
   IF NOT EXISTS (SELECT * FROM [dbo].[tb_c_countries] 
                   WHERE country_code = 'TH')
   BEGIN
       INSERT INTO [dbo].[tb_c_countries]
           ([country]
           ,[country_code]
           ,[currency_symbol]
           ,[is_active])
		 VALUES
			   ('TH'
			   ,'TH'
			   ,'THB'
			   ,1)
   END
END

BEGIN
   IF NOT EXISTS (SELECT * FROM [dbo].[tb_c_countries] 
                   WHERE country_code = 'IN')
   BEGIN
       INSERT INTO [dbo].[tb_c_countries]
           ([country]
           ,[country_code]
           ,[currency_symbol]
           ,[is_active])
		 VALUES
			   ('IN'
			   ,'IN'
			   ,'INR'
			   ,1)
   END
END

BEGIN
   IF NOT EXISTS (SELECT * FROM [dbo].[tb_c_countries] 
                   WHERE country_code = 'ID')
   BEGIN
       INSERT INTO [dbo].[tb_c_countries]
           ([country]
           ,[country_code]
           ,[currency_symbol]
           ,[is_active])
		 VALUES
			   ('ID'
			   ,'ID'
			   ,'IDR'
			   ,1)
   END
END

BEGIN
   IF NOT EXISTS (SELECT * FROM [dbo].[tb_c_countries] 
                   WHERE country_code = 'JP')
   BEGIN
       INSERT INTO [dbo].[tb_c_countries]
           ([country]
           ,[country_code]
           ,[currency_symbol]
           ,[is_active])
		 VALUES
			   ('JP'
			   ,'JP'
			   ,'JPY'
			   ,1)
   END
END

BEGIN
   IF NOT EXISTS (SELECT * FROM [dbo].[tb_c_countries] 
                   WHERE country_code = 'MA')
   BEGIN
       INSERT INTO [dbo].[tb_c_countries]
           ([country]
           ,[country_code]
           ,[currency_symbol]
           ,[is_active])
		 VALUES
			   ('MA'
			   ,'MA'
			   ,'MAD'
			   ,1)
   END
END

BEGIN
   IF NOT EXISTS (SELECT * FROM [dbo].[tb_c_countries] 
                   WHERE country_code = 'TW')
   BEGIN
       INSERT INTO [dbo].[tb_c_countries]
           ([country]
           ,[country_code]
           ,[currency_symbol]
           ,[is_active])
		 VALUES
			   ('TW'
			   ,'TW'
			   ,'TWD'
			   ,1)
   END
END

BEGIN
   IF NOT EXISTS (SELECT * FROM [dbo].[tb_c_countries] 
                   WHERE country_code = 'GR')
   BEGIN
       INSERT INTO [dbo].[tb_c_countries]
           ([country]
           ,[country_code]
           ,[currency_symbol]
           ,[is_active])
		 VALUES
			   ('GR'
			   ,'GR'
			   ,'USD'
			   ,1)
   END
END

BEGIN
   IF NOT EXISTS (SELECT * FROM [dbo].[tb_c_countries] 
                   WHERE country_code = 'SG')
   BEGIN
       INSERT INTO [dbo].[tb_c_countries]
           ([country]
           ,[country_code]
           ,[currency_symbol]
           ,[is_active])
		 VALUES
			   ('SG'
			   ,'SG'
			   ,'SGD'
			   ,1)
   END
END

