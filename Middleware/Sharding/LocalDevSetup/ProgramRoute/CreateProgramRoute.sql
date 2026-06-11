BEGIN
   IF NOT EXISTS (SELECT * FROM [dbo].[tb_pr_program_route] pr 
					WHERE pr.[program_code]='0001' AND pr.[program_collection_code]='0001' AND pr.[tenant_id]=1)
   BEGIN
       
	   INSERT INTO [dbo].[tb_pr_program_route]
           ([program_code]
           ,[program_collection_code]
           ,[tenant_id])
     VALUES
           ('0001'
           ,'0001'
           ,1)
		   
   END
END

BEGIN
   IF NOT EXISTS (SELECT * FROM [dbo].[tb_pr_program_route] pr 
					WHERE pr.[program_code]='0001' AND pr.[program_collection_code]='0002' AND pr.[tenant_id]=1)
   BEGIN
       
	   INSERT INTO [dbo].[tb_pr_program_route]
           ([program_code]
           ,[program_collection_code]
           ,[tenant_id])
     VALUES
           ('0001'
           ,'0002'
           ,1)
		   
   END
END