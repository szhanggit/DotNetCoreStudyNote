$SQLServer = "localhost"
$db = "master"

$Username = "leansql"
$Password = "Edenred2021!"

invoke-sqlcmd -inputfile "ProgramRoute\CreateDatabases.sql" -serverinstance $SQLServer -database $db -Verbose

Write-Host "Finished Create Tenant Configs"