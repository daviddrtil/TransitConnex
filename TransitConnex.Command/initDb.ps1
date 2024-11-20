# Define the server and database names
$serverName = "localhost"
$databaseName = "TransitConnex"
# $sqlScriptPath = Join-Path -Path (Get-Location) -ChildPath "TransitConnext.Infrastructure\TransixConnexInitDb.sql"
$sqlScriptPath = "TransixConnexInitDb.sql"

# Define the credentials
$username = "sa"
$password = "VUTFITez2024"

# Create the database
$createDatabaseQuery = "CREATE DATABASE [$databaseName]"
Invoke-Sqlcmd -ServerInstance $serverName -Query $createDatabaseQuery -Username $username -Password $password

# Debug resolved path
Write-Output "Resolved Path: $sqlScriptPath"
Test-Path "C:\Users\popdo\RiderProjects\PDB_project\TransitConnext.Infrastructure"

# Run the SQL script to create tables
Invoke-Sqlcmd -ServerInstance $serverName -Database $databaseName -InputFile $sqlScriptPath -Username $username -Password $password
