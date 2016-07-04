Push-Location $PSScriptRoot

& dotnet restore

$revision = @{ $true = $env:APPVEYOR_BUILD_NUMBER; $false = 1 }[$env:APPVEYOR_BUILD_NUMBER -ne $NULL];

Push-Location src

& dotnet pack -c Release -o ..\.\artifacts --version-suffix=$revision
if($LASTEXITCODE -ne 0) { exit 1 }    

Pop-Location
Pop-Location

