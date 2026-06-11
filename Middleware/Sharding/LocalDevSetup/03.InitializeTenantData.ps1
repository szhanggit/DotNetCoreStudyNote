$SQLServer = "LAPTOP-LDKKTL6G\STEVENZSERVER"
$db = "txc_local_tenant"

$Username = "steven"
$Password = "steven!"

invoke-sqlcmd -inputfile "Tenant\Countries.sql" -serverinstance $SQLServer -database $db -Verbose
invoke-sqlcmd -inputfile "Tenant\Tenant.sql" -serverinstance $SQLServer -database $db -Verbose
invoke-sqlcmd -inputfile "Tenant\TenantConfig_ContainerName.sql" -serverinstance $SQLServer -database $db -Verbose
invoke-sqlcmd -inputfile "Tenant\TenantConfig_TX2ConnectorQueueName.sql" -serverinstance $SQLServer -database $db -Verbose

Write-Host "Finished Create Tenant Data"