$SQLServer = "LAPTOP-LDKKTL6G\STEVENZSERVER"
$db = "master"

$Username = "steven"
$Password = "steven"

invoke-sqlcmd -inputfile "Tenant\CreateDatabases.sql" -serverinstance $SQLServer -database $db 

Write-Host "Finished Create DBs"