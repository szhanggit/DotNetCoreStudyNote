USE master
GO

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'txc_local_tenant')
BEGIN
  CREATE DATABASE txc_local_tenant;
  PRINT N'Created txc_local_tenant'
END;
GO

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'txc_local_tenant_th')
BEGIN
  CREATE DATABASE txc_local_tenant_th;
  PRINT N'Created txc_local_tenant_th'
END;
GO

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'txc_local_tenant_in')
BEGIN
  CREATE DATABASE txc_local_tenant_in;
  PRINT N'Created txc_local_tenant_in'
END;
GO

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'txc_local_tenant_id')
BEGIN
  CREATE DATABASE txc_local_tenant_id;
  PRINT N'Created txc_local_tenant_id'
END;
GO

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'txc_local_tenant_jp')
BEGIN
  CREATE DATABASE txc_local_tenant_jp;
  PRINT N'Created txc_local_tenant_jp'
END;
GO

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'txc_local_tenant_gr')
BEGIN
  CREATE DATABASE txc_local_tenant_gr;
  PRINT N'Created txc_local_tenant_gr'
END;
GO

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'txc_local_tenant_sg')
BEGIN
  CREATE DATABASE txc_local_tenant_sg;
  PRINT N'Created txc_local_tenant_sg'
END;
GO

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'txc_local_tenant_tw')
BEGIN
  CREATE DATABASE txc_local_tenant_tw;
  PRINT N'Created txc_local_tenant_tw'
END;
GO
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'txc_local_tenant_ma')
BEGIN
  CREATE DATABASE txc_local_tenant_ma;
  PRINT N'Created txc_local_tenant_ma'
END;
GO
