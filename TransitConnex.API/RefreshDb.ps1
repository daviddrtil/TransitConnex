#PowerShell -ExecutionPolicy Bypass -File .\RefreshDb.ps1

dotnet ef migrations remove  --project ..\TransitConnex.Command\TransitConnex.Command.csproj
dotnet ef migrations add InitDb --project ..\TransitConnex.Command\TransitConnex.Command.csproj
dotnet run --launch-profile "http-unseed"
dotnet ef database update --project ..\TransitConnex.Command\TransitConnex.Command.csproj
dotnet run --launch-profile "http-seed"

#dotnet run --launch-profile "http-unseed" ; dotnet ef database update --project ..\TransitConnex.Command\TransitConnex.Command.csproj ; dotnet run --launch-profile "http-seed"
