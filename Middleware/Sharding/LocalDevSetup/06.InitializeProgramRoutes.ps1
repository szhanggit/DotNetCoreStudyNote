$SQLServer = "localhost"
$db = "txc_local_tenant"

$Username = "leansql"
$Password = "Edenred2021!"

invoke-sqlcmd -inputfile "ProgramRoute\CreateProgramRoute.sql" -serverinstance $SQLServer -database $db -Verbose

Write-Host "Finished Create Tenant Configs"