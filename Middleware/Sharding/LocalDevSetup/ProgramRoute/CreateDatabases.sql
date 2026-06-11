USE master
GO

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'txc_local_th_programcollection_0001')
BEGIN
  CREATE DATABASE txc_local_th_programcollection_0001;
  PRINT N'Created txc_local_th_programcollection_0001'
END;
GO

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'txc_local_th_programcollection_0002')
BEGIN
  CREATE DATABASE txc_local_th_programcollection_0002;
  PRINT N'Created txc_local_th_programcollection_0002'
END;
GO
