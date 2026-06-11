
BEGIN
   IF NOT EXISTS (SELECT * FROM [dbo].[tb_tc_tenant_config] tc 
					JOIN tb_tbi_tenant_basic_information tbi
					ON tc.tenant_id = tbi.tenant_basic_info_id
					WHERE tc.configname='TX2ConnectorQueueName' AND tbi.country_code='GR')
   BEGIN
       INSERT INTO [dbo].[tb_tc_tenant_config]
           ([tenant_id]
           ,[configtype]
           ,[configname]
           ,[version]
           ,[value]
           ,[comment]
           ,[creation_time])
			SELECT
			[tenant_basic_info_id],
			'Common',
			'TX2ConnectorQueueName',
			1,
			'tx2connectorqueue-gr-development'
			,''
			,GETUTCDATE()
			FROM tb_tbi_tenant_basic_information
			WHERE [country_code] = 'GR';
   END
END

BEGIN
   IF NOT EXISTS (SELECT * FROM [dbo].[tb_tc_tenant_config] tc 
					JOIN tb_tbi_tenant_basic_information tbi
					ON tc.tenant_id = tbi.tenant_basic_info_id
					WHERE tc.configname='TX2ConnectorQueueName' AND tbi.country_code='ID')
   BEGIN
       INSERT INTO [dbo].[tb_tc_tenant_config]
           ([tenant_id]
           ,[configtype]
           ,[configname]
           ,[version]
           ,[value]
           ,[comment]
           ,[creation_time])
			SELECT
			[tenant_basic_info_id],
			'Common',
			'TX2ConnectorQueueName',
			1,
			'tx2connectorqueue-gl-development'
			,''
			,GETUTCDATE()
			FROM tb_tbi_tenant_basic_information
			WHERE [country_code] = 'ID';
   END
END

BEGIN
   IF NOT EXISTS (SELECT * FROM [dbo].[tb_tc_tenant_config] tc 
					JOIN tb_tbi_tenant_basic_information tbi
					ON tc.tenant_id = tbi.tenant_basic_info_id
					WHERE tc.configname='TX2ConnectorQueueName' AND tbi.country_code='IN')
   BEGIN
       INSERT INTO [dbo].[tb_tc_tenant_config]
           ([tenant_id]
           ,[configtype]
           ,[configname]
           ,[version]
           ,[value]
           ,[comment]
           ,[creation_time])
			SELECT
			[tenant_basic_info_id],
			'Common',
			'TX2ConnectorQueueName',
			1,
			'tx2connectorqueue-in-development'
			,''
			,GETUTCDATE()
			FROM tb_tbi_tenant_basic_information
			WHERE [country_code] = 'IN';
   END
END

BEGIN
   IF NOT EXISTS (SELECT * FROM [dbo].[tb_tc_tenant_config] tc 
					JOIN tb_tbi_tenant_basic_information tbi
					ON tc.tenant_id = tbi.tenant_basic_info_id
					WHERE tc.configname='TX2ConnectorQueueName' AND tbi.country_code='JP')
   BEGIN
       INSERT INTO [dbo].[tb_tc_tenant_config]
           ([tenant_id]
           ,[configtype]
           ,[configname]
           ,[version]
           ,[value]
           ,[comment]
           ,[creation_time])
			SELECT
			[tenant_basic_info_id],
			'Common',
			'TX2ConnectorQueueName',
			1,
			'tx2connectorqueue-gl-development'
			,''
			,GETUTCDATE()
			FROM tb_tbi_tenant_basic_information
			WHERE [country_code] = 'JP';
   END
END

BEGIN
   IF NOT EXISTS (SELECT * FROM [dbo].[tb_tc_tenant_config] tc 
					JOIN tb_tbi_tenant_basic_information tbi
					ON tc.tenant_id = tbi.tenant_basic_info_id
					WHERE tc.configname='TX2ConnectorQueueName' AND tbi.country_code='MA')
   BEGIN
       INSERT INTO [dbo].[tb_tc_tenant_config]
           ([tenant_id]
           ,[configtype]
           ,[configname]
           ,[version]
           ,[value]
           ,[comment]
           ,[creation_time])
			SELECT
			[tenant_basic_info_id],
			'Common',
			'TX2ConnectorQueueName',
			1,
			'tx2connectorqueue-gl-development'
			,''
			,GETUTCDATE()
			FROM tb_tbi_tenant_basic_information
			WHERE [country_code] = 'MA';
   END
END

BEGIN
   IF NOT EXISTS (SELECT * FROM [dbo].[tb_tc_tenant_config] tc 
					JOIN tb_tbi_tenant_basic_information tbi
					ON tc.tenant_id = tbi.tenant_basic_info_id
					WHERE tc.configname='TX2ConnectorQueueName' AND tbi.country_code='SG')
   BEGIN
       INSERT INTO [dbo].[tb_tc_tenant_config]
           ([tenant_id]
           ,[configtype]
           ,[configname]
           ,[version]
           ,[value]
           ,[comment]
           ,[creation_time])
			SELECT
			[tenant_basic_info_id],
			'Common',
			'TX2ConnectorQueueName',
			1,
			'tx2connectorqueue-sg-development'
			,''
			,GETUTCDATE()
			FROM tb_tbi_tenant_basic_information
			WHERE [country_code] = 'SG';
   END
END

BEGIN
   IF NOT EXISTS (SELECT * FROM [dbo].[tb_tc_tenant_config] tc 
					JOIN tb_tbi_tenant_basic_information tbi
					ON tc.tenant_id = tbi.tenant_basic_info_id
					WHERE tc.configname='TX2ConnectorQueueName' AND tbi.country_code='TH')
   BEGIN
       INSERT INTO [dbo].[tb_tc_tenant_config]
           ([tenant_id]
           ,[configtype]
           ,[configname]
           ,[version]
           ,[value]
           ,[comment]
           ,[creation_time])
			SELECT
			[tenant_basic_info_id],
			'Common',
			'TX2ConnectorQueueName',
			1,
			'tx2connectorqueue-gl-development'
			,''
			,GETUTCDATE()
			FROM tb_tbi_tenant_basic_information
			WHERE [country_code] = 'TH';
   END
END

BEGIN
   IF NOT EXISTS (SELECT * FROM [dbo].[tb_tc_tenant_config] tc 
					JOIN tb_tbi_tenant_basic_information tbi
					ON tc.tenant_id = tbi.tenant_basic_info_id
					WHERE tc.configname='TX2ConnectorQueueName' AND tbi.country_code='TW')
   BEGIN
       INSERT INTO [dbo].[tb_tc_tenant_config]
           ([tenant_id]
           ,[configtype]
           ,[configname]
           ,[version]
           ,[value]
           ,[comment]
           ,[creation_time])
			SELECT
			[tenant_basic_info_id],
			'Common',
			'TX2ConnectorQueueName',
			1,
			'tx2connectorqueue-tw-development'
			,''
			,GETUTCDATE()
			FROM tb_tbi_tenant_basic_information
			WHERE [country_code] = 'TW';
   END
END
